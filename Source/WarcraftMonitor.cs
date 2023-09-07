﻿using System.Diagnostics;
using System.Runtime.InteropServices;

namespace InventoryHotkeys
{
    /// <summary>
    /// Periodically checks whether Warcraft III is open and whether the user is in normal gameplay.
    /// </summary>
    class WarcraftMonitor
    {
        [DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr srchDc, int srcX, int srcY, int srcW, int srcH,
            IntPtr desthDc, int destX, int destY, int op);

        // TODO: Need to check if we're actually in a game. Currently,
        // this will return false if the chat input field is open, otherwise true.
        public static bool IsPlaying()
        {
            if (SearchForWarcraft(out Process warcraftProcess))
            {
                return IsWarcraftGameplay(warcraftProcess);
            }

            return false;
        }

        private static bool SearchForWarcraft(out Process process)
        {
            var processes = Process.GetProcessesByName("Warcraft III");

            if (processes.Length > 0)
            {
                process = processes[0];
                return true;
            }

            process = null;
            return false;
        }

        // We only want to forward key presses when the user is in normal gameplay;
        // otherwise, they will not be able to correctly type chat messages.

        // Checks if the message input field is open by comparing pixel values.
        // We can tell that the window is open when the yellow opaque border is present
        // on the screen.

        // TODO: Probably doesn't work for anything other than 1920x1080, and maybe issues
        // with different gamma settings (does Reforged even have a gamma slider?).
        private static bool IsWarcraftGameplay(Process warcraftProcess)
        {
            IntPtr handle = warcraftProcess.MainWindowHandle;

            using var bitmap = new Bitmap(512, 1);

            using Graphics gdest = Graphics.FromImage(bitmap);
            using Graphics gsrc = Graphics.FromHwnd(handle);

            IntPtr hsrcdc = gsrc.GetHdc();
            IntPtr hdc = gdest.GetHdc();

            BitBlt(hdc, 0, 0, bitmap.Width, 1, hsrcdc, 620, 841, (int)CopyPixelOperation.SourceCopy);

            gdest.ReleaseHdc();
            gsrc.ReleaseHdc();

            // Count how many pixels in the area match. Some may be incorrect due to the mouse cursor
            // hovering the message box; to account for this, we assume if more than 64 are incorrect
            // the message box is closed.
            Color a = Color.FromArgb(255, 245, 194, 37), b = Color.FromArgb(255, 246, 194, 37);

            int count = 0;

            for (int i = 0; i < bitmap.Width; i++)
            {
                Color pixel = bitmap.GetPixel(i, 0);

                if (pixel != a && pixel != b)
                {
                    count++;
                }
            }

            return count > 64;
        }
    }
}

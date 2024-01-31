using System.Diagnostics;
using System.Runtime.InteropServices;

namespace InventoryHotkeys;

/// <summary>
/// Checks whether Warcraft III is open and whether the user is in normal gameplay.
/// </summary>
class WarcraftMonitor
{
    [DllImport("gdi32.dll")]
    private static extern int BitBlt(IntPtr srchDc, int srcX, int srcY, int srcW, int srcH,
        IntPtr desthDc, int destX, int destY, int op);

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern IntPtr GetForegroundWindow();

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

    private static bool IsWarcraftGameplay(Process warcraftProcess)
    {
        IntPtr handle = warcraftProcess.MainWindowHandle;
        IntPtr foreground = GetForegroundWindow();

        // Check if Warcraft is focused.
        return handle == foreground;
    }
}

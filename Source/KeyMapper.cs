using InventoryHotkeys;
using Warcraft3InventoryHotkeys.Source;

namespace Warcraft3InventoryHotkeys;

class KeyMapper
{
    public static bool hotkeysEnabled, hotkeysPolling;
    public KeyMapper()
    {
    }

    public static void SetBindings(Config config)
    {
        bindings[config.Numpad7] = VirtualNumpad.Numpad._7;
        bindings[config.Numpad8] = VirtualNumpad.Numpad._8;
        bindings[config.Numpad4] = VirtualNumpad.Numpad._4;
        bindings[config.Numpad5] = VirtualNumpad.Numpad._5;
        bindings[config.Numpad1] = VirtualNumpad.Numpad._1;
        bindings[config.Numpad2] = VirtualNumpad.Numpad._2;
    }

    private static readonly Dictionary<Keys, VirtualNumpad.Numpad> bindings = new()
    {
    };

    public static void OnKeyPressed(int vCode)
    {
        // Home key.
        if (vCode == 36)
        {
            hotkeysEnabled = !hotkeysEnabled;
        }
        var k = (Keys)vCode;
        Console.WriteLine($"[{(Keys)vCode}]");
        if (hotkeysEnabled && hotkeysPolling)
        {
            if (bindings.ContainsKey((Keys)vCode))
            {
                VirtualNumpad.PressDown(bindings[(Keys)vCode]);
            }
        }
    }
}

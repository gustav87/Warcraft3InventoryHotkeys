namespace Warcraft3InventoryHotkeys;

public class Numpad
{
    public Numpad(string value) { Value = value; }
    public string Value { get; private set; }
    public static Numpad NUMPAD7 { get { return new Numpad("Numpad 7"); } }
    public static Numpad NUMPAD8 { get { return new Numpad("Numpad 8"); } }
    public static Numpad NUMPAD4 { get { return new Numpad("Numpad 4"); } }
    public static Numpad NUMPAD5 { get { return new Numpad("Numpad 5"); } }
    public static Numpad NUMPAD1 { get { return new Numpad("Numpad 1"); } }
    public static Numpad NUMPAD2 { get { return new Numpad("Numpad 2"); } }

    public override string ToString()
    {
        return Value;
    }
}

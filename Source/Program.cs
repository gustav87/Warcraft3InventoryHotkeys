using InventoryHotkeys;
using Warcraft3InventoryHotkeys;
using Warcraft3InventoryHotkeys.Source;

class Program
{
    private static InterceptKeys interceptor;
    private static StatusWindow statusWindow;
    private static Gui gui;
    private static Config config;

    static void Main()
    {
        config = Config.Load();
        KeyMapper.SetBindings(config);

        interceptor = new();
        interceptor.OnPress += KeyMapper.OnKeyPressed;
        interceptor.Register();

        KeyMapper.hotkeysEnabled = true;

        statusWindow = new(config.WindowLocation);
        statusWindow.OnMoved += StatusWindow_OnMoved;

        gui = new();

        new Task(Monitor).Start();

        // Application.Run(gui);
        // Application.Run(statusWindow);
        Application.Run(new MultiFormContext(statusWindow, gui));

        interceptor.Dispose();
    }

    private static void StatusWindow_OnMoved()
    {
        config.WindowLocation = statusWindow.Location;

        Config.Save(config);
    }

    static async void Monitor()
    {
        while (true)
        {
            KeyMapper.hotkeysPolling = WarcraftMonitor.IsPlaying();

            StatusWindow.IndicatorStatus indicator;

            if (!KeyMapper.hotkeysEnabled)
                indicator = StatusWindow.IndicatorStatus.Disabled;
            else if (KeyMapper.hotkeysPolling)
                indicator = StatusWindow.IndicatorStatus.Polling;
            else
                indicator = StatusWindow.IndicatorStatus.Idle;

            statusWindow.SetIndicator(indicator);

            await Task.Delay(100);
        }
    }

    // private static readonly Dictionary<Keys, VirtualNumpad.Numpad> bindings = new()
    // {
    //     {  Keys.Oem6,  VirtualNumpad.Numpad._7 },
    //     {  Keys.N,  VirtualNumpad.Numpad._2 },
    // };
}

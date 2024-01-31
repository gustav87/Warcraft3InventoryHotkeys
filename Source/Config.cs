using Newtonsoft.Json;

namespace Warcraft3InventoryHotkeys.Source;

public class Config
{
    public Point WindowLocation;
    public Keys Numpad7;
    public Keys Numpad8;
    public Keys Numpad4;
    public Keys Numpad5;
    public Keys Numpad1;
    public Keys Numpad2;

    private const string name = "w3c-inventory-keys-config.json";

    public static Config Load()
    {
        if (!File.Exists(name))
        {
            return new();
        }
        else
        {
            string json = File.ReadAllText(name);                
            return JsonConvert.DeserializeObject<Config>(json);
        }
    }

    public static void Save(Config config)
    {
        string json = JsonConvert.SerializeObject(config);

        File.WriteAllText(name, json);
    }
}

using Warcraft3InventoryHotkeys.Source;

namespace Warcraft3InventoryHotkeys;

class Gui: Form
{
    private static Config config;
    public Gui()
    {
        config = Config.Load();
        TopMost = true;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Size = new Size(400, 400);
        StartPosition = FormStartPosition.CenterScreen;

        TextBox numpad7TextBox = new TextBox
        {
            Location = new Point(70, 10),
            Enabled = true,
            Width = 50,
            Visible = true,
            Name = Numpad.NUMPAD7.ToString(),
            Text = config.Numpad7.ToString(),
        };
        TextBox numpad8TextBox = new TextBox
        {
            Location = new Point(190, 10),
            Enabled = true,
            Height = 80,
            Width = 50,
            Visible = true,
            Name = Numpad.NUMPAD8.ToString(),
            Text = config.Numpad8.ToString(),
        };
        TextBox numpad4TextBox = new TextBox
        {
            Location = new Point(70, 40),
            Enabled = true,
            Height = 80,
            Width = 50,
            Visible = true,
            Name = Numpad.NUMPAD4.ToString(),
            Text = config.Numpad4.ToString(),
        };
        TextBox numpad5TextBox = new TextBox
        {
            Location = new Point(190, 40),
            Enabled = true,
            Height = 80,
            Width = 50,
            Visible = true,
            Name = Numpad.NUMPAD5.ToString(),
            Text = config.Numpad5.ToString(),
        };
        TextBox numpad1TextBox = new TextBox
        {
            Location = new Point(70, 70),
            Enabled = true,
            Height = 80,
            Width = 50,
            Visible = true,
            Name = Numpad.NUMPAD1.ToString(),
            Text = config.Numpad1.ToString(),
        };
        TextBox numpad2TextBox = new TextBox
        {
            Location = new Point(190, 70),
            Enabled = true,
            Height = 80,
            Width = 50,
            Visible = true,
            Name = Numpad.NUMPAD2.ToString(),
            Text = config.Numpad2.ToString(),
        };
        Label numpad7Label = new Label
        {
            Location = new Point(10, 10),
            Width = 70,
            Visible = true,
            Text = Numpad.NUMPAD7.ToString(),
        };
        Label numpad8Label = new Label
        {
            Location = new Point(130, 10),
            Width = 70,
            Visible = true,
            Text = Numpad.NUMPAD8.ToString(),
        };
        Label numpad4Label = new Label
        {
            Location = new Point(10, 40),
            Width = 70,
            Visible = true,
            Text = Numpad.NUMPAD4.ToString(),
        };
        Label numpad5Label = new Label
        {
            Location = new Point(130, 40),
            Width = 70,
            Visible = true,
            Text = Numpad.NUMPAD5.ToString(),
        };
        Label numpad1Label = new Label
        {
            Location = new Point(10, 70),
            Width = 70,
            Visible = true,
            Text = Numpad.NUMPAD1.ToString(),
        };
        Label numpad2Label = new Label
        {
            Location = new Point(130, 70),
            Width = 70,
            Visible = true,
            Text = Numpad.NUMPAD2.ToString(),
        };

        Controls.Add(numpad7TextBox);
        Controls.Add(numpad8TextBox);
        Controls.Add(numpad4TextBox);
        Controls.Add(numpad5TextBox);
        Controls.Add(numpad1TextBox);
        Controls.Add(numpad2TextBox);

        Controls.Add(numpad7Label);
        Controls.Add(numpad8Label);
        Controls.Add(numpad4Label);
        Controls.Add(numpad5Label);
        Controls.Add(numpad1Label);
        Controls.Add(numpad2Label);

        numpad7TextBox.KeyDown += OnKeyDown;
        numpad8TextBox.KeyDown += OnKeyDown;
        numpad4TextBox.KeyDown += OnKeyDown;
        numpad5TextBox.KeyDown += OnKeyDown;
        numpad1TextBox.KeyDown += OnKeyDown;
        numpad2TextBox.KeyDown += OnKeyDown;

        // txtBox.KeyPress += OnKeyPress;

        numpad7TextBox.KeyUp += OnKeyUp;
        numpad8TextBox.KeyUp += OnKeyUp;
        numpad4TextBox.KeyUp += OnKeyUp;
        numpad5TextBox.KeyUp += OnKeyUp;
        numpad1TextBox.KeyUp += OnKeyUp;
        numpad2TextBox.KeyUp += OnKeyUp;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        TextBox t = (TextBox)sender;
        t.Text = "";
        Keys k = (Keys)e.KeyValue;
        SaveConfig(t.Name, k);
        KeyMapper.SetBindings(config);
        t.Text = t.Text.ToUpper();
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        TextBox t = (TextBox)sender;
        t.Text = t.Text.ToUpper();
    }

    private static void SaveConfig(string textBoxName, Keys key)
    {
        switch (textBoxName) {
            case "Numpad 7":
                config.Numpad7 = key;
                break;
            case "Numpad 8":
                config.Numpad8 = key;
                break;
            case "Numpad 4":
                config.Numpad4 = key;
                break;
            case "Numpad 5":
                config.Numpad5 = key;
                break;
            case "Numpad 1":
                config.Numpad1 = key;
                break;
            case "Numpad 2":
                config.Numpad2 = key;
                break;
        }
        Config.Save(config);
    }
}

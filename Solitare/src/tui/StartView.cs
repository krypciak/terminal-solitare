namespace Solitare;

using Terminal.Gui;

/// <summary>
/// The start screen of the game.
/// </summary>
public class StartView : Window
{
    private Pos baseX = Pos.Center() - 18;
    private Pos baseY = Pos.Center() - 4;

    private TextValidateField seedTextField;
    private RadioGroup difficultyRadio;

#pragma warning disable CS8618
    public StartView()
#pragma warning restore CS8618
    {
        this.Width = Dim.Fill(0);
        this.Height = Dim.Fill(0);
        this.X = 0;
        this.Y = 0;

        this.Visible = true;
        this.Arrangement = ViewArrangement.Overlapped;
        this.CanFocus = true;
        this.ShadowStyle = ShadowStyle.None;
        this.Modal = false;
        this.TextAlignment = Alignment.Start;
        this.Title = "Pasjans gigathon 2025 (Kliknij Esc by wyjść)";

        this.ColorScheme = new ColorScheme(
            new Attribute(Color.Black, Color.Green),
            new Attribute(Color.Black, Color.BrightGreen),
            new Attribute(Color.Black, Color.Green),
            new Attribute(Color.Black, Color.Green),
            new Attribute(Color.Black, Color.Green)
        );

        InitializeSeedLabel();
        InitializeSeedTextField();
        InitializeDifficultyLabel();
        InitializeDifficultyRadio();
        InitializeStartButton();
        InitializeLeaderboard();
    }

    private void InitializeSeedLabel()
    {
        var seedLabel = new Label()
        {
            Width = Dim.Auto(),
            Height = Dim.Auto(),
            X = baseX,
            Y = baseY + 4,
            Visible = true,
            Arrangement = ViewArrangement.Fixed,
            CanFocus = false,
            ShadowStyle = ShadowStyle.None,
            Data = "seedLabel",
            Text = "Ziarno gry (seed)",
            TextAlignment = Alignment.Start,
        };
        this.Add(seedLabel);
    }

    private void InitializeSeedTextField()
    {
        this.seedTextField = new TextValidateField()
        {
            Width = 15,
            Height = 1,
            X = baseX,
            Y = baseY + 5,
            Visible = true,
            Arrangement = ViewArrangement.Fixed,
            CanFocus = true,
            ShadowStyle = ShadowStyle.None,
            Data = "seedTextField",
            Text = "123",
            TextAlignment = Alignment.Center,
            ColorScheme = new ColorScheme(
                new Attribute(Color.Black, Color.Green),
                new Attribute(Color.Black, Color.BrightGreen),
                new Attribute(Color.Black, Color.Green),
                new Attribute(Color.Black, Color.Green),
                new Attribute(Color.Black, Color.Green)
            ),
            Provider = new Terminal.Gui.TextValidateProviders.TextRegexProvider("")
            {
                Pattern = "^[0-9]+$",
                Text = "123"
            },
        };
        this.Add(seedTextField);
    }

    private void InitializeDifficultyLabel()
    {
        var difficultyLabel = new Label()
        {
            Width = Dim.Auto(),
            Height = Dim.Auto(),
            X = baseX,
            Y = baseY,
            Visible = true,
            Arrangement = ViewArrangement.Fixed,
            CanFocus = false,
            ShadowStyle = ShadowStyle.None,
            Data = "difficultyLabel",
            Text = "Poziom trudności:",
            TextAlignment = Alignment.Start,
        };
        Add(difficultyLabel);
    }

    private void InitializeDifficultyRadio()
    {
        this.difficultyRadio = new RadioGroup()
        {
            Width = 10,
            Height = 2,
            X = baseX,
            Y = baseY + 2,
            Visible = true,
            Arrangement = ViewArrangement.Fixed,
            CanFocus = true,
            ShadowStyle = ShadowStyle.None,
            Data = "difficultyRadio",
            TextAlignment = Alignment.Start,
            RadioLabels = new string[] { "Łatwy", "Trudny" },
        };
        Add(difficultyRadio);

    }

    private void InitializeStartButton()
    {
        var startButton = new Button()
        {
            Width = Dim.Auto(),
            Height = Dim.Auto(),
            X = baseX,
            Y = baseY + 7,
            Visible = true,
            Arrangement = ViewArrangement.Fixed,
            ColorScheme = this.ColorScheme,
            CanFocus = true,
            ShadowStyle = ShadowStyle.Transparent,
            Data = "startButton",
            Text = ">Rozpocznij grę<",
            TextAlignment = Alignment.Center,
            IsDefault = false,
            NoDecorations = true,
        };
        this.Add(startButton);

        startButton.Accepting += (s, e) =>
        {
            if (!seedTextField.IsValid) return;

            int index = difficultyRadio.SelectedItem;
            Difficulty difficulty = index == 0 ? Difficulty.Easy : Difficulty.Hard;

            var seed = int.Parse(seedTextField.Text);
            var game = new Game(seed, difficulty);
            var view = new GameView(game);
            Application.Run(view);
        };
    }

    private void InitializeLeaderboard()
    {
        var leaderboard = new LeaderboardView(baseX + 40, baseY + 6);
        this.Add(leaderboard);
    }
}


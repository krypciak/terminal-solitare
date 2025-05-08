namespace solitare
{
    using Terminal.Gui;
    using Color = Terminal.Gui.Color;

    public partial class GameView : Terminal.Gui.Window
    {

        private Terminal.Gui.ColorScheme greenScheme;

        public Terminal.Gui.Label invalidMoveLabel;

        private Terminal.Gui.MenuBarv2 menu;
        private Terminal.Gui.Shortcut undoShortcut;

        private void InitializeComponent()
        {
            this.greenScheme = new Terminal.Gui.ColorScheme(
                new Terminal.Gui.Attribute(Color.Black, Color.Green),
                new Terminal.Gui.Attribute(Color.Black, Color.Green),
                new Terminal.Gui.Attribute(Color.Black, Color.Green),
                new Terminal.Gui.Attribute(Color.Black, Color.Green),
                new Terminal.Gui.Attribute(Color.Black, Color.Green)
            );
            this.Width = Dim.Fill(0);
            this.Height = Dim.Fill(0);
            this.X = 0;
            this.Y = 0;
            this.Visible = true;
            this.Arrangement = Terminal.Gui.ViewArrangement.Overlapped;
            this.ColorScheme = this.greenScheme;
            this.CanFocus = true;
            this.ShadowStyle = Terminal.Gui.ShadowStyle.None;
            this.Modal = false;
            this.TextAlignment = Terminal.Gui.Alignment.Start;
            this.Title = "Pasjans gigathon 2025 (Kliknij Esc by wyjść)";

            this.invalidMoveLabel = new Terminal.Gui.Label();
            this.invalidMoveLabel.X = Pos.Absolute(3);
            this.invalidMoveLabel.Y = Pos.Absolute(1);
            this.invalidMoveLabel.CanFocus = false;
            this.invalidMoveLabel.Visible = true;
            this.invalidMoveLabel.TextAlignment = Terminal.Gui.Alignment.Start;
            this.invalidMoveLabel.Title = "";
            this.invalidMoveLabel.ColorScheme = new Terminal.Gui.ColorScheme(new Terminal.Gui.Attribute(Color.Red, Color.White));
            this.Add(this.invalidMoveLabel);

            menu = new Terminal.Gui.MenuBarv2();
            undoShortcut = new Shortcut
            {
                Key = Key.Z,
                HighlightStyle = HighlightStyle.Hover,
                Action = () =>
                {
                    Game.game.UndoMove();
                }
            };
            UpdateUndoShortcutText(0);

            menu.Add(undoShortcut);
            this.Add(menu);
        }

        public void UpdateUndoShortcutText(int moves)
        {
            undoShortcut.Title = $"Cofnij ruch ({moves}/{GameState.historySize})";
        }
    }
}

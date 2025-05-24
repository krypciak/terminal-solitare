namespace Solitare
{
    using Terminal.Gui;
    using Color = Terminal.Gui.Color;

    public partial class GameView : Terminal.Gui.Window
    {

        private Terminal.Gui.ColorScheme greenScheme;

        private Terminal.Gui.Label invalidMoveLabel;

        private Terminal.Gui.MenuBarv2 menu;
        private Terminal.Gui.Shortcut undoShortcut;
        private Terminal.Gui.Label moveCount;

        private void InitializeComponent()
        {
            this.greenScheme = new Terminal.Gui.ColorScheme(
                new Terminal.Gui.Attribute(Color.Black, Color.Green),
                new Terminal.Gui.Attribute(Color.Black, Color.BrightGreen),
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
            this.Title = "Pasjans gigathon 2025";

            this.invalidMoveLabel = new Terminal.Gui.Label();
            this.invalidMoveLabel.X = Pos.Absolute(0);
            this.invalidMoveLabel.Y = Pos.Absolute(1);
            this.invalidMoveLabel.CanFocus = false;
            this.invalidMoveLabel.Visible = true;
            this.invalidMoveLabel.TextAlignment = Terminal.Gui.Alignment.Start;
            this.invalidMoveLabel.Title = "";
            this.invalidMoveLabel.ColorScheme = new Terminal.Gui.ColorScheme(new Terminal.Gui.Attribute(Color.Red, Color.White));
            this.Add(this.invalidMoveLabel);

            menu = new Terminal.Gui.MenuBarv2
            {
                ColorScheme = new Terminal.Gui.ColorScheme(
                    new Terminal.Gui.Attribute(Color.Black, Color.BrightGreen)
                )
            };
            menu.Add(new Shortcut()
            {
                Title = "Wyjdź do menu głównego",
                Key = Key.Esc,
                HighlightStyle = HighlightStyle.Hover,
                Action = () => Application.RequestStop()
            });

            menu.Add(new Label { Title = " | ", CanFocus = false, });

            undoShortcut = new Shortcut
            {
                Key = Key.Z,
                HighlightStyle = HighlightStyle.Hover,
                Action = () =>
                {
                    game.UndoMove();
                    FullRedraw();
                }
            };
            UpdateUndoShortcutText(0);
            menu.Add(undoShortcut);

            menu.Add(new Label { Title = " | ", CanFocus = false, });

            moveCount = new Label();
            UpdateMoveCountText(0);
            menu.Add(moveCount);

            this.Add(menu);
        }

        public void UpdateUndoShortcutText(int moves)
        {
            undoShortcut.Title = $"Cofnij ruch ({moves}/{Game.MAX_HISTORY_SIZE})";
        }

        public void UpdateMoveCountText(int moveCount)
        {
            this.moveCount.Title = $"Liczba ruchów: {moveCount}";
        }

        public async void SetInvalidMoveMessage(string error)
        {
            this.invalidMoveLabel.Title = $" Zły ruch: {error}";
            var currentInvalidMoveCount = ++invalidMoveCount;
            await Task.Delay(5000);
            if (currentInvalidMoveCount == invalidMoveCount)
            {
                this.invalidMoveLabel.Title = "";
            }
        }
    }
}

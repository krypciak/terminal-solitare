using Terminal.Gui;
using Color = Terminal.Gui.Color;

namespace solitare
{
    public class CardBaseView : Button
    {
        public const int WIDTH = 7;
        public const int HEIGHT = 4;

        public CardBaseView(Pos x, Pos y, bool focusable)
        {
            this.X = x;
            this.Y = y;
            this.Visible = true;
            this.CanFocus = focusable;
            this.Enabled = true;
            this.ShadowStyle = ShadowStyle.Opaque;
            this.HighlightStyle = HighlightStyle.None;
            this.NoDecorations = true;
            this.NoPadding = true;

            this.TextAlignment = Terminal.Gui.Alignment.Start;
            this.Text = $"       " + '\n' +
                        $" pusty " + '\n' +
                        $"       " + '\n' +
                        $"       " + '\n';

            this.ColorScheme =
                new Terminal.Gui.ColorScheme(
                        new Terminal.Gui.Attribute(Color.White, Color.DarkGray),
                        new Terminal.Gui.Attribute(Color.Black, Color.Yellow),
                        new Terminal.Gui.Attribute(Color.White, Color.DarkGray),
                        new Terminal.Gui.Attribute(Color.White, Color.DarkGray),
                        new Terminal.Gui.Attribute(Color.White, Color.DarkGray)
                );
        }
    }
}

using Terminal.Gui;

namespace myproj
{
    public class CardBaseView : Terminal.Gui.Label
    {
        public CardBaseView(Pos x, Pos y, bool focusable)
        {
            this.X = x;
            this.Y = y;
            this.Width = Dim.Auto();
            this.Height = Dim.Auto();
            this.Visible = true;
            this.CanFocus = focusable;
            this.Enabled = true;

            this.TextAlignment = Terminal.Gui.Alignment.Start;
            this.Text = $"       " + '\n' +
                        $" empty " + '\n' +
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

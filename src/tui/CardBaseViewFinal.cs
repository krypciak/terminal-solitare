using Terminal.Gui;

namespace solitare
{
    public class CardBaseViewFinal : CardBaseView
    {
        public CardBaseViewFinal(Pos x, Pos y, bool focusable, CardType type) : base(x, y, focusable)
        {
            this.X = x;
            this.Y = y;
            this.Width = Dim.Auto();
            this.Height = Dim.Auto();
            this.Visible = true;
            this.CanFocus = focusable;
            this.Enabled = true;

            this.TextAlignment = Terminal.Gui.Alignment.Start;
            char typeSymbol = CardView.cardTypeToSymbol[type];
            this.Text = $"{typeSymbol}    {typeSymbol} " + '\n' +
                        $"       " + '\n' +
                        $"       " + '\n' +
                        $"{typeSymbol}    {typeSymbol} " + '\n';

            var color = CardView.cardTypeToColor[type];

            this.ColorScheme =
                new Terminal.Gui.ColorScheme(
                        new Terminal.Gui.Attribute(color, Color.Gray),
                        new Terminal.Gui.Attribute(color, Color.Yellow),
                        new Terminal.Gui.Attribute(color, Color.Gray),
                        new Terminal.Gui.Attribute(color, Color.Gray),
                        new Terminal.Gui.Attribute(color, Color.Gray)
                );
        }
    }
}

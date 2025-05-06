using Terminal.Gui;

namespace myproj
{
    public class CardView : CardBaseView
    {
        private static Dictionary<CardType, Color> cardTypeToColor = new Dictionary<CardType, Color>() {
            { CardType.Karo, Color.Red},
            { CardType.Kier, Color.Red},
            { CardType.Pik, Color.Black},
            { CardType.Trefl, Color.Black},
        };

        public static int width = 7;
        public static int height = 4;

        private Card card;

        public CardView(Card card, Pos x, Pos y, bool hidden, int deckPosition, bool focusable) : base(x, y, focusable)
        {
            this.card = card;
            this.Enabled = !hidden;
            this.Text = $"{card.typeSymbol}  {card.rankSymbol}{card.typeSymbol} " + '\n' +
                        $"       " + '\n' +
                        $"       " + '\n' +
                        $"{card.typeSymbol}  {card.rankSymbol}{card.typeSymbol} " + '\n';

            var color = hidden ? Color.Gray : cardTypeToColor[card.type];
            var disabledColor = deckPosition % 2 == 0 ? Color.DarkGray : Color.Gray;

            this.ColorScheme =
                new Terminal.Gui.ColorScheme(
                        new Terminal.Gui.Attribute(color, Color.White),
                        new Terminal.Gui.Attribute(color, Color.BrightYellow),
                        new Terminal.Gui.Attribute(color, Color.White),
                        new Terminal.Gui.Attribute(disabledColor, disabledColor),
                        new Terminal.Gui.Attribute(color, Color.White)
                );
        }
    }
}

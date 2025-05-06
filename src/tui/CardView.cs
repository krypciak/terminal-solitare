using Terminal.Gui;

namespace myproj
{
    public class CardView : CardBaseView
    {
        public static Dictionary<CardType, Color> cardTypeToColor = new Dictionary<CardType, Color>() {
            { CardType.Karo, Color.Red},
            { CardType.Kier, Color.Red},
            { CardType.Pik, Color.Black},
            { CardType.Trefl, Color.Black},
        };

        public static Dictionary<CardType, char> cardTypeToSymbol = new Dictionary<CardType, char>() {
            { CardType.Karo, '♦'},
            { CardType.Kier, '♥'},
            { CardType.Pik, '♠'},
            { CardType.Trefl, '♣'},
        };

        public static Dictionary<CardRank, string> cardRankToSymbol = new Dictionary<CardRank, string>() {
            { CardRank.Krol, "K " },
            { CardRank.Dama, "Q " },
            { CardRank.Walet, "J " },
            { CardRank.K10, "10" },
            { CardRank.K9, "9 " },
            { CardRank.K8, "8 " },
            { CardRank.K7, "7 " },
            { CardRank.K6, "6 " },
            { CardRank.K5, "5 " },
            { CardRank.K4, "4 " },
            { CardRank.K3, "3 " },
            { CardRank.K2, "2 " },
            { CardRank.As, "A " },
        };

        public static int width = 7;
        public static int height = 4;

        public Card card;
        public int deckPosition;

        public CardView(Card card, Pos x, Pos y, bool hidden, int deckPosition, bool focusable) : base(x, y, focusable)
        {
            this.card = card;
            this.deckPosition = deckPosition;
            SetEnabled(!hidden);

            char typeSymbol = cardTypeToSymbol[card.type];
            string rankSymbol = cardRankToSymbol[card.rank];
            this.Text = $"{typeSymbol}  {rankSymbol}{typeSymbol} " + '\n' +
                        $"       " + '\n' +
                        $"       " + '\n' +
                        $"{typeSymbol}  {rankSymbol}{typeSymbol} " + '\n';
        }

        public void SetEnabled(bool enabled)
        {
            this.Enabled = enabled;
            this.CanFocus = enabled;
            var color = enabled ? cardTypeToColor[card.type] : Color.Gray;
            var disabledColor = deckPosition % 2 == 0 ? Color.DarkGray : Color.Gray;

            this.ColorScheme =
                new Terminal.Gui.ColorScheme(
                        new Terminal.Gui.Attribute(color, Color.White),
                        new Terminal.Gui.Attribute(Color.Red, Color.BrightYellow),
                        new Terminal.Gui.Attribute(color, Color.White),
                        new Terminal.Gui.Attribute(disabledColor, disabledColor),
                        new Terminal.Gui.Attribute(color, Color.White)
                );
        }
    }
}

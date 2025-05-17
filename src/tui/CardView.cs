using Terminal.Gui;

namespace solitare
{
    public class CardView : CardBaseView
    {
        public static Dictionary<CardType, Color> cardTypeToColor = new()  {
            { CardType.Karo, Color.Red},
            { CardType.Kier, Color.Red},
            { CardType.Pik, Color.Black},
            { CardType.Trefl, Color.Black},
        };

        public static Dictionary<CardType, char> cardTypeToSymbol = new() {
            { CardType.Karo, '♦'},
            { CardType.Kier, '♥'},
            { CardType.Pik, '♠'},
            { CardType.Trefl, '♣'},
        };

        public static Dictionary<CardRank, string> cardRankToSymbol = new() {
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

        public Card card;
        public int deckPosition;

        public CardView(Card card, Pos x, Pos y, bool hidden, int deckPosition, bool focusable) : base(x, y, focusable)
        {
            this.card = card;
            this.deckPosition = deckPosition;
            this.Enabled = card.uncovered;

            char typeSymbol = cardTypeToSymbol[card.type];
            string rankSymbol = cardRankToSymbol[card.rank];
            this.Text = $"{typeSymbol}  {rankSymbol}{typeSymbol} " + '\n' +
                        $"       " + '\n' +
                        $"       " + '\n' +
                        $"{typeSymbol}  {rankSymbol}{typeSymbol} " + '\n';


            var color = card.uncovered ? cardTypeToColor[card.type] : Color.Gray;
            var disabledColor = deckPosition % 2 == 0 ? Color.DarkGray : Color.Gray;

            this.ColorScheme =
                new Terminal.Gui.ColorScheme(
                        new Terminal.Gui.Attribute(color, Color.White),
                        new Terminal.Gui.Attribute(color, Color.BrightYellow),
                        new Terminal.Gui.Attribute(color, Color.White),
                        new Terminal.Gui.Attribute(disabledColor, disabledColor),
                        new Terminal.Gui.Attribute(color, Color.White)
                );

            this.Accepting += (s, e) =>
            {
                if (GameView.selectedCard == null)
                {
                    GameView.selectedCard = this;
                }
            };
        }
    }
}

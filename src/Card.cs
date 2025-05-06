namespace myproj
{
    public class Card
    {

        private static Dictionary<CardType, char> cardTypeToSymbol = new Dictionary<CardType, char>()
        {
            { CardType.Karo, '♦'},
            { CardType.Kier, '♥'},
            { CardType.Pik, '♠'},
            { CardType.Trefl, '♣'},
        };

        private static Dictionary<CardRank, string> cardRankToSymbol = new Dictionary<CardRank, string>()
        {
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

        public CardType type { get; private set; }
        public CardRank rank { get; private set; }
        public char typeSymbol;
        public string rankSymbol;

        public Card(CardType type, CardRank rank)
        {
            this.type = type;
            this.rank = rank;
            typeSymbol = cardTypeToSymbol[type];
            rankSymbol = cardRankToSymbol[rank];
        }
    }
}

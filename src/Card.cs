namespace myproj
{
    public class Card
    {
        public CardType type { get; private set; }
        public CardRank rank { get; private set; }

        public Card(CardType type, CardRank rank)
        {
            this.type = type;
            this.rank = rank;
        }
    }
}

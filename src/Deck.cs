namespace myproj
{
    public abstract class Deck
    {
        public Card[] cards;
        public bool showOnlyLast;

        public Deck(Card[] cards)
        {
            this.cards = cards;
        }
    }
}


namespace myproj
{
    public abstract class Deck
    {
        public List<Card> cards;
        public bool showOnlyLast;

        public Deck(List<Card> cards)
        {
            this.cards = cards;
        }

        abstract public bool CanMoveCardHere(Card card);

        public void PushCard(Card card)
        {
            if (!CanMoveCardHere(card)) throw new Exception("called PushCard, but can move here!");

            this.cards.Add(card);
        }
    }
}


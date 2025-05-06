namespace myproj
{
    public class DeckFinal : Deck
    {
        private CardType cardType;

        public DeckFinal(Card[] cards, CardType cardType) : base(cards)
        {
            this.cardType = cardType;
        }

    }
}

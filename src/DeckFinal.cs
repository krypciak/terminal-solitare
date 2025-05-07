namespace solitare
{
    public class DeckFinal : Deck
    {
        public CardType cardType;

        public DeckFinal(List<Card> cards, CardType cardType) : base(cards)
        {
            this.cardType = cardType;
        }

        public override bool CanMoveCardHere(Card card)
        {
            if (card.type != this.cardType) return false;
            if (cards.Count == 0)
            {
                return card.rank == CardRank.Krol;
            }
            else
            {
                var topCard = cards.Last();
                return topCard.rank == card.rank - 1;
            }
        }
    }
}

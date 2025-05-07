namespace solitare
{
    public class DeckFinal : Deck
    {
        public CardType cardType;

        public DeckFinal(List<Card> cards) : base(cards)
        {
        }

        public override bool CanMoveCardHere(Card card)
        {
            if (cards.Count == 0)
            {
                return card.rank == CardRank.As;
            }
            else
            {
                var topCard = cards.Last();
                return topCard.type == card.type && topCard.rank == card.rank + 1;
            }
        }
    }
}

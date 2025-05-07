namespace solitare
{
    public class DeckInitial : Deck
    {
        public DeckInitial(List<Card> cards) : base(cards) { }

        public override bool CanMoveCardHere(Card card)
        {
            return false;
        }

    }
}

namespace myproj
{
    public class DeckReserve : Deck
    {
        public DeckReserve(List<Card> cards) : base(cards) { }

        public override bool CanMoveCardHere(Card card)
        {
            return false;
        }
    }
}

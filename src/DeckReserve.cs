using FluentResults;

namespace solitare
{
    public class DeckReserve : Deck
    {
        public DeckReserve(List<Card> cards) : base(cards) { }

        public override Result CanMoveCardHere(Card card)
        {
            return Result.Fail("Nie wolno przenosić kart na stos rezerwowy!");
        }
    }
}

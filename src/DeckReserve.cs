using FluentResults;

namespace solitare
{
    public class DeckReserve : Deck
    {
        private List<Card> hiddenCards = new List<Card>();

        public DeckReserve(List<Card> cards) : base(cards) { }

        public override Result CanMoveCardHere(Card card)
        {
            return Result.Fail("Nie wolno przenosić kart na stos rezerwowy!");
        }

        public void NextCard()
        {
            if (Game.game == null) return;

            if (cards.Count == 0)
            {
                if (hiddenCards.Count == 0) return;

                cards = Game.game.ShuffleCards(hiddenCards);
                hiddenCards.Clear();
            }
            else
            {
                var topCard = cards.Last();
                this.PopCard(cards.Count - 1);
                hiddenCards.Add(topCard);
            }
        }
    }
}

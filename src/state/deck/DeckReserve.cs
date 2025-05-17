using System.Text.Json.Serialization;
using FluentResults;

namespace solitare
{
    public class DeckReserve : Deck
    {
        [JsonInclude]
        private List<Card> hiddenCards = new List<Card>();

        public DeckReserve(List<Card> cards) : base(cards) { }

        public override Result CanMoveCardHere(Card card)
        {
            return Result.Fail("Nie wolno przenosić kart na stos rezerwowy!");
        }

        public void NextCard()
        {
            if (cards.Count == 0)
            {
                if (hiddenCards.Count == 0) return;

                if (Game.instance!.reserveShowCount == 1)
                {
                    cards.Clear();
                    cards.AddRange(Game.instance!.ShuffleCards(hiddenCards));
                }
                else
                {
                    cards.Clear();
                    cards.AddRange(new List<Card>(hiddenCards));
                    cards.Reverse();
                }
                hiddenCards.Clear();
            }
            else
            {
                for (int i = 0; i < Game.instance!.reserveShowCount && cards.Count > 0; i++)
                {
                    var topCard = cards.Last();
                    this.PopCards(1);
                    hiddenCards.Add(topCard);
                }
            }
        }
    }
}

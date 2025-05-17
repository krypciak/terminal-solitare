using System.Text.Json.Serialization;
using FluentResults;

namespace solitare
{
    public abstract class Deck
    {
        [JsonInclude]
        public List<Card> cards { get; }

        public Deck(List<Card> cards)
        {
            this.cards = cards;
        }

        public abstract Result CanMoveCardHere(Card card);

        public void PushCards(List<Card> cards)
        {
            if (CanMoveCardHere(cards[0]).IsFailed) throw new Exception("called PushCard, but cant move here!");

            this.cards.AddRange(cards);
        }

        public void PopCards(int count)
        {
            cards.RemoveRange(cards.Count - count, count);
            if (cards.Count > 0)
            {
                cards.Last().uncovered = true;
            }
        }
    }
}


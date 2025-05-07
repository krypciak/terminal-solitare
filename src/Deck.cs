using FluentResults;

namespace solitare
{
    public abstract class Deck
    {
        public List<Card> cards;

        public Deck(List<Card> cards)
        {
            this.cards = cards;
        }

        abstract public Result CanMoveCardHere(Card card);

        public void PushCard(Card card)
        {
            if (CanMoveCardHere(card).IsFailed) throw new Exception("called PushCard, but cant move here!");

            cards.Add(card);
        }

        public void PopCard(int at)
        {
            cards.RemoveRange(at, this.cards.Count - at);
            if (cards.Count > 0)
            {
                cards.Last().uncovered = true;
            }
        }
    }
}


using FluentResults;

namespace solitare
{
    public abstract class Deck
    {
        public List<Card> cards;
        public bool showOnlyLast;

        public Deck(List<Card> cards)
        {
            this.cards = cards;
        }

        abstract public Result CanMoveCardHere(Card card);

        public void PushCard(Card card)
        {
            if (CanMoveCardHere(card).IsFailed) throw new Exception("called PushCard, but cant move here!");

            this.cards.Add(card);
        }

        public void PopCard(int at)
        {
            this.cards.RemoveRange(at, this.cards.Count - at);
        }
    }
}


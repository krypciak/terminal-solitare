using Terminal.Gui;

namespace solitare
{
    public interface IDeckView
    {
        Deck deck { get; }
        Stack<CardView> cardViews { get; }

        void PushCardView(CardView cardView);
        void PopCardView();
    }

    abstract public class DeckView<T> : Terminal.Gui.View, IDeckView where T : Deck
    {
        protected T _deck;
        public Deck deck => _deck;
        public Stack<CardView> cardViews { get; private set; } = [];
        protected CardBaseView? baseView;

        public DeckView(T deck, Pos x, Pos y)
        {
            this._deck = deck;
            this.X = x;
            this.Y = y;
            this.Width = Dim.Auto();
            this.Height = Dim.Auto();
            this.Visible = true;
            this.CanFocus = true;

            CreateCardViews();

            this.Accepting += (s, e) =>
            {
                if (GameView.selectedDeck == null)
                {
                    if (cardViews.TryPeek(out CardView? view))
                    {
                        view.SetFocus();
                        GameView.selectedDeck = this;
                    }
                }
                else if (Game.game != null)
                {
                    Game.game.TryMoveCard(this);
                    if (cardViews.Count > 0) GameView.selectedDeck = this;
                }

            };
        }

        abstract protected (int, int) GetCardPositionByDeckPosition(int deckPosition);
        abstract protected bool ShoudCardBeHidden(int deckPosition);
        abstract protected void CreateBaseView();

        private void CreateCardViews()
        {
            var cardCount = _deck.cards.Count;

            CreateBaseView();
            if (baseView != null) { this.Add(baseView); }

            for (int i = 0; i < cardCount; i++)
            {
                var card = _deck.cards[i];
                bool last = i == cardCount - 1;
                var (x, y) = GetCardPositionByDeckPosition(i);
                var hidden = ShoudCardBeHidden(i);
                var view = new CardView(card, x, y, hidden, i, last);
                cardViews.Push(view);
                this.Add(view);
            }

        }

        public void PushCardView(CardView cardView)
        {
            if (cardViews.TryPeek(out CardView? view))
            {
                view.CanFocus = false;
            }
            else if (baseView != null)
            {
                baseView.CanFocus = false;
            }

            cardView.deckPosition = cardViews.Count;
            var (x, y) = this.GetCardPositionByDeckPosition(cardView.deckPosition);
            cardView.X = x;
            cardView.Y = y;
            cardViews.Push(cardView);
            this.Add(cardView);
        }

        public void PopCardView()
        {
            var view = cardViews.Pop();
            this.Remove(view);

            if (cardViews.TryPeek(out view))
            {
                view.SetEnabled(true);
            }
            else if (baseView != null)
            {
                baseView.CanFocus = true;
            }
        }
    }
}

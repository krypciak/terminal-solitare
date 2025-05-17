using Terminal.Gui;

namespace solitare
{
    public interface IDeckView
    {
        Deck deck { get; }
        Stack<CardView> cardViews { get; }

        void ClearFocus();
        void FullRedraw();
    }

    public abstract class DeckView<T> : Terminal.Gui.View, IDeckView where T : Deck
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
                    if (cardViews.Count > 0) GameView.selectedDeck = this;
                }
                else if (GameView.selectedCard != null && GameView.selectedDeck != this)
                {
                    GameView.selectedDeck.ClearFocus();
                    Game.instance!.TryMoveCard(this);
                }
            };
        }

        abstract protected (int, int) GetCardPositionByDeckPosition(Card card, int i);
        abstract protected bool ShouldCardBeHidden(Card card, int i);
        abstract protected bool ShouldCardBeFocusable(Card card, int i);
        abstract protected bool ShouldDisableFocusOnPushCardView();

        public void ClearFoucs()
        {
            base.ClearFocus();
            baseView?.ClearFocus();
            foreach (var view in cardViews) view.ClearFocus();
        }

        public virtual void FullRedraw()
        {
            this.Remove(baseView);
            foreach (var view in cardViews) this.Remove(view);
            this.CreateCardViews();
        }

        protected void CreateCardViews()
        {
            var cardCount = _deck.cards.Count;

            baseView = new CardBaseView(0, 0, _deck.cards.Count == 0);
            this.Add(baseView);

            for (int i = 0; i < cardCount; i++)
            {
                var card = _deck.cards[i];
                var (x, y) = GetCardPositionByDeckPosition(card, i);
                var focusable = ShouldCardBeFocusable(card, i);
                var hidden = ShouldCardBeHidden(card, i);
                var view = new CardView(card, x, y, hidden, i, focusable);
                cardViews.Push(view);
                this.Add(view);
            }

        }
    }
}

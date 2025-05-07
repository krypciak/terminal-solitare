using Terminal.Gui;

namespace solitare
{
    public class DeckViewReserve : DeckView<DeckReserve>
    {
        private Action<bool> updateButtonText;

        public DeckViewReserve(DeckReserve deck, Pos x, Pos y, Action<bool> updateButtonText) : base(deck, x, y)
        {
            this.updateButtonText = updateButtonText;
        }

        override protected (int, int) GetCardPositionByDeckPosition(Card card, int i)
        {
            int deckPosRev = _deck.cards.Count - i - 1;
            if (deckPosRev >= Game.game!.reserveShowCount) return (0, 0);
            return (deckPosRev * (CardView.width + 1), 0);
        }

        override protected bool ShouldCardBeHidden(Card card, int i)
        {
            int deckPosRev = _deck.cards.Count - i - 1;
            return deckPosRev >= Game.game!.reserveShowCount;
        }

        protected override bool ShouldCardBeFocusable(Card card, int i)
        {
            return i == _deck.cards.Count - 1;
        }

        protected override bool ShouldDisableFocusOnPushCardView()
        {
            return true;
        }

        public override void FullRedraw()
        {
            base.FullRedraw();

            updateButtonText(_deck.cards.Count == 0);
        }

        public void NextCard()
        {
            _deck.NextCard();

            this.FullRedraw();
        }
    }
}

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

        override protected (int, int) GetCardPositionByDeckPosition(int deckPosition)
        {
            int deckPosRev = _deck.cards.Count - deckPosition - 1;
            if (deckPosRev >= Game.game!.reserveShowCount) return (0, 0);
            return (deckPosRev * (CardView.width + 1), 0);
        }
        override protected bool ShouldCardBeHidden(int deckPosition)
        {
            int deckPosRev = _deck.cards.Count - deckPosition - 1;
            return deckPosRev >= Game.game!.reserveShowCount;
        }

        protected override bool ShouldCardBeFocusable(int deckPosition)
        {
            return deckPosition == _deck.cards.Count - 1;
        }

        protected override bool ShouldDisableFocusOnPushCardView()
        {
            return true;
        }

        public void NextCard()
        {
            _deck.NextCard();

            updateButtonText(_deck.cards.Count == 0);

            this.FullRedraw();
        }
    }
}

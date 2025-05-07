using Terminal.Gui;

namespace solitare
{
    public class DeckViewReserve : DeckView<DeckReserve>
    {
        private ReserveView nextButton;

        public DeckViewReserve(DeckReserve deck, Pos x, Pos y) : base(deck, x, y)
        {
            nextButton = new ReserveView(CardView.width + 1, 0, (s, e) =>
            {
                this.NextCard();
            });
            this.Add(nextButton);
        }

        override protected (int, int) GetCardPositionByDeckPosition(int deckPosition)
        {
            return (0, 0);
        }
        override protected bool ShouldCardBeHidden(int deckPosition)
        {
            return deckPosition != _deck.cards.Count - 1;
        }

        protected override void CreateBaseView()
        {
            baseView = new CardBaseView(0, 0, _deck.cards.Count == 0);
        }

        protected override bool ShouldDisableFocusOnPushCardView()
        {
            return true;
        }

        public void NextCard()
        {
            _deck.NextCard();

            nextButton.SetEmpty(_deck.cards.Count == 0);

            this.FullRedraw();
        }
    }
}

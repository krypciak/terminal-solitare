using Terminal.Gui;

namespace solitare
{
    public class DeckViewFinal : DeckView<DeckFinal>
    {
        public DeckViewFinal(DeckFinal deck, Pos x, Pos y) : base(deck, x, y) { }

        override protected (int, int) GetCardPositionByDeckPosition(int deckPosition)
        {
            return (0, 0);
        }

        protected override bool ShouldCardBeHidden(int deckPosition)
        {
            return false;
        }

        protected override void CreateBaseView()
        {
            baseView = new CardBaseView(0, 0, _deck.cards.Count == 0);
        }

        protected override bool ShouldDisableFocusOnPushCardView()
        {
            return true;
        }
    }
}

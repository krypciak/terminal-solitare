using Terminal.Gui;

namespace solitare
{
    public class DeckViewFinal : DeckView<DeckFinal>
    {
        public DeckViewFinal(DeckFinal deck, Pos x, Pos y) : base(deck, x, y) { }

        override protected (int, int) GetCardPositionByDeckPosition(Card card, int i)
        {
            return (0, 0);
        }

        protected override bool ShouldCardBeHidden(Card card, int i)
        {
            return i != _deck.cards.Count - 1;
        }

        protected override bool ShouldCardBeFocusable(Card card, int i)
        {
            return !ShouldCardBeHidden(card, i);
        }

        protected override bool ShouldDisableFocusOnPushCardView()
        {
            return true;
        }

    }
}

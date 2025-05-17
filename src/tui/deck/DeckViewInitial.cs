using Terminal.Gui;

namespace solitare
{
    public class DeckViewInitial : DeckView<DeckInitial>
    {
        public DeckViewInitial(DeckInitial deck, Pos x, Pos y) : base(deck, x, y) { }


        override protected (int, int) GetCardPositionByDeckPosition(Card card, int i)
        {
            return (0, i);
        }
        override protected bool ShouldCardBeHidden(Card card, int i)
        {
            return i != _deck.cards.Count - 1;
        }

        protected override bool ShouldCardBeFocusable(Card card, int i)
        {
            return card.uncovered;
        }

        protected override bool ShouldDisableFocusOnPushCardView()
        {
            return false;
        }
    }
}

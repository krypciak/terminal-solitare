using Terminal.Gui;

namespace solitare
{
    public class DeckViewReserve : DeckView<DeckReserve>
    {
        public DeckViewReserve(DeckReserve deck, Pos x, Pos y) : base(deck, x, y) { }

        override protected (int, int) GetCardPositionByDeckPosition(int deckPosition)
        {
            return (0, 0);
        }
        override protected bool ShoudCardBeHidden(int deckPosition)
        {
            return deckPosition != _deck.cards.Count - 1;
        }

        protected override void CreateBaseView()
        {
            baseView = new CardBaseView(0, 0, _deck.cards.Count == 0);
        }
    }
}

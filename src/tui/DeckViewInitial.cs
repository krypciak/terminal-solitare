using Terminal.Gui;

namespace myproj
{
    public class DeckViewInitial : DeckView<DeckInitial>
    {
        public DeckViewInitial(DeckInitial deck, Pos x, Pos y) : base(deck, x, y) { }


        override protected (int, int) GetCardPositionByDeckPosition(int deckPosition)
        {
            return (0, deckPosition);
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

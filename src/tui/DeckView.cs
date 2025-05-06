using Terminal.Gui;

namespace myproj
{
    abstract public class DeckView : Terminal.Gui.View
    {
        protected Deck deck;
        protected CardView[] cardViews = [];

        public DeckView(Deck deck, Pos x, Pos y)
        {
            this.deck = deck;
            this.X = x;
            this.Y = y;
            this.Width = Dim.Auto();
            this.Height = Dim.Auto();
            this.Visible = true;
            this.CanFocus = true;

            CreateCardViews();
        }

        abstract protected void CreateCardViews();
    }
}

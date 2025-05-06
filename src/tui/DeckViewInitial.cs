using Terminal.Gui;

namespace myproj
{
    public class DeckViewInitial : DeckView
    {
        public DeckViewInitial(Deck deck, Pos x, Pos y) : base(deck, x, y) { }

        protected override void CreateCardViews()
        {
            var cardCount = deck.cards.Length;

            var baseView = new CardBaseView(0, 0, cardCount == 0);
            this.Add(baseView);

            for (int i = 0; i < cardCount; i++)
            {
                var card = deck.cards[i];
                bool last = i == cardCount - 1;
                var view = new CardView(card, 0, i, !last, i, last);
                cardViews.Append(view);
                this.Add(view);
            }
        }

    }

}

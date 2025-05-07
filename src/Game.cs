using Terminal.Gui;

namespace solitare
{
    public enum Difficulty
    {
        Easy,
        Hard
    }

    public class Game
    {
        public static Game? game;

        private GameView view;
        private Difficulty difficulty;
        private GameState state;

        public Game(Difficulty difficulty)
        {
            Game.game = this;

            state = new GameState(123);

            view = new GameView(state);
            this.difficulty = difficulty;

            Application.Run(view);
        }

        public void TryMoveCard(IDeckView to)
        {
            if (GameView.selectedDeck == null) throw new Exception("invalid call! selectedCard is null");
            var cardView = GameView.selectedDeck.cardViews.Peek();
            var card = cardView.card;
            if (to.deck.CanMoveCardHere(card))
            {
                GameView.selectedDeck.PopCardView();

                to.deck.PushCard(card);
                to.PushCardView(cardView);
            }
            else
            {
                GameView.selectedDeck.cardViews.Peek().ClearFocus();
                GameView.selectedDeck = null;
            }
        }
    }
}

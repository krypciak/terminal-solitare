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
            if (GameView.selectedDeck == null) throw new Exception("invalid call! selectedDeck is null");
            if (GameView.selectedCard == null) throw new Exception("invalid call! selectedCard is null");

            var selCard = GameView.selectedCard.card;

            var result = to.deck.CanMoveCardHere(selCard);
            if (result.IsSuccess)
            {
                GameView.selectedDeck.PopCardView();
                GameView.selectedDeck.deck.PopCard(GameView.selectedDeck.deck.cards.Count - 1);

                to.deck.PushCard(selCard);
                to.PushCardView(GameView.selectedCard);
            }
            else
            {
                var error = result.Errors[0].Message;
                MessageBox.ErrorQuery(50, 8, "Nieprawidłowy ruch!", $"\n{error}", "Ok");
            }
            GameView.selectedDeck?.ClearFocus();
            GameView.selectedDeck = null;
            GameView.selectedCard = null;
        }
    }
}

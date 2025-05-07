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
        private GameState state;

        public int reserveShowCount;

        public Game(Difficulty difficulty)
        {
            Game.game = this;

            state = new GameState(123, difficulty);
            this.reserveShowCount = difficulty == Difficulty.Easy ? 1 : 3;

            view = new GameView(state);

            Application.Run(view);
        }

        public List<Card> ShuffleCards(List<Card> cards) => state.ShuffleCards(cards);

        public void TryMoveCard(IDeckView to)
        {
            if (GameView.selectedDeck == null) throw new Exception("invalid call! selectedDeck is null");
            if (GameView.selectedCard == null) throw new Exception("invalid call! selectedCard is null");

            var selCard = GameView.selectedCard.card;

            var result = to.deck.CanMoveCardHere(selCard);
            if (result.IsSuccess)
            {
                GameView.selectedDeck.deck.PopCard(GameView.selectedDeck.deck.cards.Count - 1);
                GameView.selectedDeck.FullRedraw();

                to.deck.PushCard(selCard);
                to.FullRedraw();
            }
            else
            {
                var error = result.Errors[0].Message;
                // MessageBox.ErrorQuery(50, 8, "Nieprawidłowy ruch!", $"\n{error}", "Ok");
                this.view.invalidMoveLabel.Title = error;
            }
            GameView.selectedDeck?.ClearFocus();
            GameView.selectedDeck = null;
            GameView.selectedCard = null;
        }
    }
}

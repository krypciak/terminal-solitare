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

        public Game(GameState state)
        {
            Game.game = this;

            this.state = state;

            this.reserveShowCount = state.difficulty == Difficulty.Easy ? 1 : 3;

            view = new GameView(state);

            Application.Run(view);
        }

        public List<Card> ShuffleCards(List<Card> cards) => state.ShuffleCards(cards);

        public void TryMoveCard(IDeckView to)
        {
            if (GameView.selectedDeck == null) throw new Exception("invalid call! selectedDeck is null");
            if (GameView.selectedCard == null) throw new Exception("invalid call! selectedCard is null");

            var selCard = GameView.selectedCard.card;
            var selDeck = GameView.selectedDeck.deck;

            var result = state.TryMoveCard(selCard, selDeck, to.deck);
            if (result.IsSuccess)
            {
                GameView.selectedDeck.FullRedraw();
                to.FullRedraw();
                view.UpdateUndoShortcutText(state.stateHistory.Count);
                view.UpdateMoveCountText(state.moveCount);
            }
            else
            {
                var error = result.Errors[0].Message;
                this.view.SetInvalidMoveMessage(error);
            }

            GameView.selectedDeck?.ClearFocus();
            GameView.selectedDeck = null;
            GameView.selectedCard = null;

            if (state.IsGameFinished())
            {
                MessageBox.Query(50, 7, "Wygrałeś!", $"Liczna ruchów: {state.moveCount}", "Wyjdź do menu głównego");
                Application.RequestStop();
            }
        }

        public void UndoMove()
        {
            if (this.state.UndoMove())
            {
                view.FullRedraw(this.state);
                view.UpdateUndoShortcutText(state.stateHistory.Count);
                view.UpdateMoveCountText(state.moveCount);
            }

        }
    }
}

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
        public static Game? instance;

        private GameView view;
        private GameState state;

        public int reserveShowCount;

        public Game(GameState state)
        {
            Game.instance = this;

            this.state = state;

            this.reserveShowCount = state.difficulty == Difficulty.Easy ? 1 : 3;

            view = new GameView(state);

            Application.Run(view);
        }

        public List<Card> ShuffleCards(List<Card> cards) => state.ShuffleCards(cards);


        private void UpdateTopBarText()
        {
            view.UpdateUndoShortcutText(state.stateHistory.Count);
            view.UpdateMoveCountText(state.moveCount);
        }

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
                UpdateTopBarText();
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

        public void NextReserveCard()
        {
            state.CommitMove();
            UpdateTopBarText();
            view.reserveDeckView.NextCard();
        }

        public void UndoMove()
        {
            if (this.state.UndoMove())
            {
                UpdateTopBarText();
                view.FullRedraw(this.state);
            }

        }
    }
}

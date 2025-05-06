using Terminal.Gui;

namespace myproj
{
    public enum Difficulty
    {
        Easy,
        Hard
    }

    public class Game
    {

        private GameView view;
        private Difficulty difficulty;
        private GameState state;

        public Game(Difficulty difficulty)
        {
            state = new GameState(123);

            view = new GameView(state);
            this.difficulty = difficulty;

            Application.Run(view);
        }
    }
}

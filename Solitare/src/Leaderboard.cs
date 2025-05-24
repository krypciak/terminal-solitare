using System.Data;

namespace Solitare
{

    public struct LeaderboardEntry
    {
        public int moves;
        public Difficulty difficulty;
    }

    public class Leaderboard
    {
        public static Leaderboard instance = new Leaderboard();

        public event Action? OnLeaderboardChange;

        private List<LeaderboardEntry> data = new();

        private Leaderboard()
        {
        }

        public DataTable CreateDataTable()
        {
            var dataTable = new DataTable("Tablica wyników");

            dataTable.Columns.Add("Liczba ruchów");
            dataTable.Columns.Add("Poziom trudności");

            foreach (var entry in data)
            {
                var difficultyText = entry.difficulty switch
                {
                    Difficulty.Hard => "Trudny",
                    Difficulty.Easy => "Łatwy",
                    _ => throw new ArgumentOutOfRangeException(nameof(entry.difficulty)),
                };

                dataTable.Rows.Add(entry.moves, difficultyText);
            }

            return dataTable;
        }

        public void Register(Game game)
        {
            game.OnGameWon += () =>
            {
                PushScore(game.state.moveCount, game.difficulty);
            };

        }

        private void PushScore(int moves, Difficulty difficulty)
        {
            data.Add(new LeaderboardEntry { moves = moves, difficulty = difficulty });

            data = data.OrderBy(e => e.moves).ToList();

            OnLeaderboardChange?.Invoke();
        }
    }
}

using System.Data;

namespace Solitare;

/// <summary>
/// Data storage for <c>Leaderboard</c>.
/// </summary>
public struct LeaderboardEntry
{
    /// <summary>
    /// Moves count it took to finish the game.
    /// </param>
    public int moves;

    /// <summary>
    /// Difficulty of the game.
    /// </param>
    public Difficulty difficulty;
}

/// <summary>
/// Manages the score leaderboard.
/// </summary>
public class Leaderboard
{
    /// <value>
    /// Singleton instance of <c>Leaderboard</c>.
    /// </value>
    public static Leaderboard instance = new Leaderboard();

    /// <summary>
    /// Event that runs when the leaderboard changes.
    /// </summary>
    public event Action? OnLeaderboardChange;

    /// <value>
    /// List containing the leaderboard data.
    /// </value>
    private List<LeaderboardEntry> data = new();

    private Leaderboard() { }

    /// <summary>
    /// Create a new <c>DataTable</c>.
    /// </summary>
    /// <returns>
    /// Each time the function is called, a new table is created.
    /// </returns>
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

    /// <summary>
    /// Hook into events on <c>Game</c>.
    /// </summary>
    public void Register(Game game)
    {
        game.OnGameWon += () =>
        {
            PushScore(game.state.moveCount, game.difficulty);
        };

    }

    /// <summary>
    /// Append a new score to the leaderboard.
    /// </summary>
    /// <param name="moves">Moves count it took to finish the game.</param>
    /// <param name="difficulty">Difficulty of the game.</param>
    private void PushScore(int moves, Difficulty difficulty)
    {
        data.Add(new LeaderboardEntry { moves = moves, difficulty = difficulty });

        data = data.OrderBy(e => e.moves).ToList();

        OnLeaderboardChange?.Invoke();
    }
}

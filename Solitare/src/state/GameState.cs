using System.Text.Json;
using System.Text.Json.Serialization;

namespace Solitare;

/// <summary>
/// Contains all the information about the game state.
/// </summary>
public class GameState
{
    /// <value>
    /// Array of final decks. The length of the array is always 4.
    /// </value>
    [JsonInclude]
    public DeckFinal[] finalDecks { get; private set; }

    /// <value>
    /// Array of initial decks. The length of the array is always 7.
    /// </value>
    [JsonInclude]
    public DeckInitial[] initialDecks { get; private set; }

    /// <value>
    /// Reserve deck.
    /// </value>
    [JsonInclude]
    public DeckReserve reserveDeck { get; private set; }

    /// <value>
    /// Current move count.
    /// </value>
    [JsonInclude]
    public int moveCount;

    /// <summary>
    /// Create a new game state.
    /// </summary>
    [JsonConstructor]
    public GameState(DeckFinal[] finalDecks, DeckInitial[] initialDecks, DeckReserve reserveDeck, int moveCount)
    {
        this.finalDecks = finalDecks;
        this.initialDecks = initialDecks;
        this.reserveDeck = reserveDeck;
        this.moveCount = moveCount;
    }

    /// <summary>
    /// Serialize the entire game state to JSON.
    /// </summary>
    public string SerializeToJSON()
    {
        return JsonSerializer.Serialize(this, typeof(GameState), new JsonSerializerOptions
        {
            IncludeFields = true,
        });
    }

    /// <summary>
    /// Create <c>GameState</c> from JSON.
    /// </summary>
    public static GameState FromJSON(string json)
    {
        return JsonSerializer.Deserialize<GameState>(json)!;
    }
}

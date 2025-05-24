using System.Text.Json;
using System.Text.Json.Serialization;

namespace Solitare
{
    public class GameState
    {
        [JsonInclude]
        public DeckFinal[] finalDecks { get; private set; }

        [JsonInclude]
        public DeckInitial[] initialDecks { get; private set; }

        [JsonInclude]
        public DeckReserve reserveDeck { get; private set; }

        [JsonInclude]
        public int moveCount;

        [JsonConstructor]
        public GameState(DeckFinal[] finalDecks, DeckInitial[] initialDecks, DeckReserve reserveDeck, int moveCount)
        {
            this.finalDecks = finalDecks;
            this.initialDecks = initialDecks;
            this.reserveDeck = reserveDeck;
            this.moveCount = moveCount;
        }

        public string SerializeToJSON()
        {
            return JsonSerializer.Serialize(this, typeof(GameState), new JsonSerializerOptions
            {
                IncludeFields = true,
            });
        }

        public static GameState FromJSON(string json)
        {
            return JsonSerializer.Deserialize<GameState>(json)!;
        }
    }
}

using System.Text.Json.Serialization;

namespace Solitare;

public class Card
{
    [JsonInclude]
    public CardType type { get; }
    [JsonInclude]
    public CardRank rank { get; }
    [JsonInclude]
    public bool uncovered = true;

    public Card(CardType type, CardRank rank)
    {
        this.type = type;
        this.rank = rank;
    }
}

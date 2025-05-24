using System.Text.Json.Serialization;

namespace Solitare;

/// <summary>
/// Stores the state of a single card.
/// </summary>
public class Card
{
    /// <value>
    /// The type of the card.
    /// </value>
    [JsonInclude]
    public CardType type { get; }

    /// <value>
    /// The rank of the card.
    /// </value>
    [JsonInclude]
    public CardRank rank { get; }

    /// <value>
    /// Is the card visible.
    /// </value>
    [JsonInclude]
    public bool uncovered = true;

    /// <summary>
    /// Create a new card.
    /// </summary>
    /// <param name="type">Card type.</param>
    /// <param name="rank">Card rank.</param>
    public Card(CardType type, CardRank rank)
    {
        this.type = type;
        this.rank = rank;
    }
}

using System.Text.Json.Serialization;
using FluentResults;

namespace Solitare;

/// <summary>
/// Represents a collection of cards.
/// Defines rules that determine what cards can be moved onto the deck.
/// </summary>
public abstract class Deck
{

    /// <value>
    /// The list of cards.
    /// </value>
    [JsonInclude]
    public List<Card> cards { get; }

    /// <summary>
    /// Creates a new deck.
    /// </summary>
    /// <param name="cards">List of cards.</param>
    public Deck(List<Card> cards)
    {
        this.cards = cards;
    }

    /// <summary>
    /// Check if <paramref name="card"/> can be moved onto the top of the deck.
    /// </summary>
    /// <param name="card">Card in question.</param>
    public abstract Result CanMoveCardHere(Card card);

    /// <summary>
    /// Push cards onto the top of the deck.
    /// This assumes the cards follow the push rules and can be pushed onto the deck.
    /// </summary>
    /// <param name="cards">List of cards to be pushed.</param>
    public void PushCards(List<Card> cards)
    {
        if (CanMoveCardHere(cards[0]).IsFailed) throw new Exception("called PushCard, but cant move here!");

        this.cards.AddRange(cards);
    }

    /// <summary>
    /// Pop a certain amount of cards from the top of the deck.
    /// </summary>
    /// <param name="count">Card count to be removed.</param>
    public void PopCards(int count)
    {
        cards.RemoveRange(cards.Count - count, count);
        if (cards.Count > 0)
        {
            cards.Last().uncovered = true;
        }
    }
}


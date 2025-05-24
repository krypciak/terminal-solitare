using System.Text.Json.Serialization;
using FluentResults;

namespace Solitare;

/// <summary>
/// Represents the deck where you draw cards from.
/// It gets reshuffled once it becomes empty.
/// </summary>
public class DeckReserve : Deck
{
    /// <value>
    /// Cards that have already been shown and are waiting for reshuffling.
    /// </value>
    [JsonInclude]
    private List<Card> hiddenCards = new List<Card>();

    /// <value>
    /// Amount of cards to show in the reserve deck.
    /// Depends on the difficulty.
    /// </value>
    [JsonInclude]
    private int reserveShowCount;

    public DeckReserve(List<Card> cards, int reserveShowCount) : base(cards)
    {
        this.reserveShowCount = reserveShowCount;
    }

    public override Result CanMoveCardHere(Card card)
    {
        return Result.Fail("Nie wolno przenosić kart na stos rezerwowy!");
    }

    /// <summary>
    /// Show the next reserve card.
    /// If the deck is empty, reshuffle it.
    /// </summary>
    public void NextCard()
    {
        if (cards.Count == 0)
        {
            if (hiddenCards.Count == 0) return;

            if (reserveShowCount == 1)
            {
                cards.Clear();
                var random = new Random();
                cards.AddRange(Game.ShuffleCards(hiddenCards, random));
            }
            else
            {
                cards.Clear();
                cards.AddRange(new List<Card>(hiddenCards));
                cards.Reverse();
            }
            hiddenCards.Clear();
        }
        else
        {
            for (int i = 0; i < reserveShowCount && cards.Count > 0; i++)
            {
                var topCard = cards.Last();
                this.PopCards(1);
                hiddenCards.Add(topCard);
            }
        }
    }
}


using FluentResults;

namespace Solitare;

/// <summary>
/// Represents the deck that contains covered cards and only only one shown card.
/// </summary>
public class DeckInitial : Deck
{
    public DeckInitial(List<Card> cards) : base(cards) { }

    public override Result CanMoveCardHere(Card card)
    {
        if (cards.Count == 0)
        {
            if (card.rank == CardRank.Krol) return Result.Ok();
            return Result.Fail("Na ten pusty stos można położyć tylko króla!");
        }
        else
        {
            var topCard = cards.Last();

            if (topCard.rank != card.rank + 1) return Result.Fail($"Karta musi być jeden niższa!");
            if (topCard.type.GetDisplayColor() == card.type.GetDisplayColor()) return Result.Fail("Karta musi być innego koloru!");
            return Result.Ok();
        }
    }

}


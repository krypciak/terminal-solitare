using FluentResults;

namespace Solitare
{
    public class DeckFinal : Deck
    {
        public DeckFinal(List<Card> cards) : base(cards) { }

        public override Result CanMoveCardHere(Card card)
        {
            if (cards.Count == 0)
            {
                if (card.rank != CardRank.As) return Result.Fail("Na ten pusty stos można połozyć tylko asa!");
                return Result.Ok();
            }
            else
            {
                var topCard = cards.Last();
                if (topCard.rank != card.rank - 1) return Result.Fail($"Karta musi być jeden wyższa!");
                if (topCard.type != card.type) return Result.Fail("Karta musi być tego samego typu!");
                return Result.Ok();
            }
        }
    }
}

using Terminal.Gui;

namespace Solitare;

public class DeckViewReserve : DeckView
{
    public DeckViewReserve(DeckReserve deck, Pos x, Pos y, Game game, Action<DeckView, CardView?> OnClick, ReserveView reserveView)
        : base(deck, x, y, game, OnClick)
    {
        game.OnDeckChange += (changedDeck) =>
        {
            if (deck != changedDeck) return;
            reserveView.SetEmpty(deck.cards.Count == 0);
        };
    }

    override protected (int, int) GetCardPositionByDeckPosition(Card card, int i)
    {
        int deckPosRev = deck.cards.Count - i - 1;
        if (deckPosRev >= game.reserveShowCount) return (0, 0);
        return (deckPosRev * (CardView.WIDTH + 1), 0);
    }

    override protected bool ShouldCardBeHidden(Card card, int i)
    {
        int deckPosRev = deck.cards.Count - i - 1;
        return deckPosRev >= game.reserveShowCount;
    }

    protected override bool ShouldCardBeFocusable(Card card, int i)
    {
        return i == deck.cards.Count - 1;
    }
}


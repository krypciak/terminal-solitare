using Terminal.Gui;

namespace Solitare;

public class DeckViewInitial : DeckView
{
    public DeckViewInitial(DeckInitial deck, Pos x, Pos y, Game game, Action<DeckView, CardView?> OnClick)
        : base(deck, x, y, game, OnClick) { }


    override protected (int, int) GetCardPositionByDeckPosition(Card card, int i)
    {
        return (0, i);
    }
    override protected bool ShouldCardBeHidden(Card card, int i)
    {
        return i != deck.cards.Count - 1;
    }

    protected override bool ShouldCardBeFocusable(Card card, int i)
    {
        return card.uncovered;
    }

    protected override bool ShouldDisableFocusOnPushCardView()
    {
        return false;
    }
}


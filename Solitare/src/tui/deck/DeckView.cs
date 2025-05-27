using Terminal.Gui;

namespace Solitare;

/// <summary>
/// Base for different <c>DeckView</c> types.
/// Each deck has it's own unique layout, and classes that inherit from <c>DeckView</c> define those rules.
/// </summary>
public abstract class DeckView : Terminal.Gui.View
{
    protected Game game;
    /// <value>
    /// List of card views that are currently displayed.
    /// </value>
    private Stack<CardView> cardViews { get; } = [];
    /// <value>
    /// The base card at the bottom of the deck saying that the deck is empty when
    /// there are no other cards in the deck.
    /// </value>
    private CardBaseView? baseView;
    private Action<DeckView, CardView> OnClick;

    public Deck deck;

    public DeckView(Deck deck, Pos x, Pos y, Game game, Action<DeckView, CardView?> OnClick)
    {
        this.game = game;
        this.deck = deck;
        this.OnClick = OnClick;
        this.X = x;
        this.Y = y;
        this.Width = Dim.Auto();
        this.Height = Dim.Auto();
        this.Visible = true;
        this.CanFocus = true;

        CreateCardViews();

        game.OnDeckChange += (changedDeck) =>
        {
            if (deck != changedDeck) return;

            FullRedraw();
        };

        this.Accepting += (s, e) =>
        {
            if (IsEmpty()) OnClick(this, null);
        };
    }

    /// <summary>
    /// Where relative to the deck view should the card view be placed
    /// </summary>
    abstract protected (int, int) GetCardPositionByDeckPosition(Card card, int i);
    /// <summary>
    /// Should the card view be hidden
    /// </summary>
    abstract protected bool ShouldCardBeHidden(Card card, int i);
    /// <summary>
    /// Should the card view be focusable
    /// </summary>
    abstract protected bool ShouldCardBeFocusable(Card card, int i);

    /// <summary>
    /// Clear focus on itself all all of it's children
    /// </summary>
    public void ClearFoucs()
    {
        base.ClearFocus();
        baseView?.ClearFocus();
        foreach (var view in cardViews) view.ClearFocus();
    }

    /// <summary>
    /// Removes all UI children and adds them again.
    /// </summary>
    public virtual void FullRedraw()
    {
        this.Remove(baseView);
        foreach (var view in cardViews) this.Remove(view);
        cardViews.Clear();
        this.CreateCardViews();
    }

    /// <summary>
    /// Create and display card views based on rules defined
    /// </summary>
    protected void CreateCardViews()
    {
        var cardCount = deck.cards.Count;

        baseView = new CardBaseView(0, 0, deck.cards.Count == 0);
        this.Add(baseView);

        for (int i = 0; i < cardCount; i++)
        {
            var card = deck.cards[i];
            var (x, y) = GetCardPositionByDeckPosition(card, i);
            var focusable = ShouldCardBeFocusable(card, i);
            var hidden = ShouldCardBeHidden(card, i);
            var view = new CardView(card, x, y, hidden, i, focusable, (cardView) => OnClick(this, cardView));
            cardViews.Push(view);
            this.Add(view);
        }
    }

    public bool IsEmpty() => cardViews.Count == 0;

    public CardView TopCardView() => cardViews.Peek();
}


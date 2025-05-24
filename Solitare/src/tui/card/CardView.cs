using Terminal.Gui;

namespace Solitare;

public class CardView : CardBaseView
{

    public Card card;

    public CardView(Card card, Pos x, Pos y, bool hidden, int deckPosition, bool focusable, Action<CardView> OnClick) : base(x, y, focusable)
    {
        this.card = card;
        this.Enabled = card.uncovered;

        var typeSymbol = card.type.GetAsciiSymbol();
        var rankSymbol = card.rank.GetAsciiText();
        this.Text = $"{typeSymbol}  {rankSymbol}{typeSymbol} " + '\n' +
                    $"       " + '\n' +
                    $"       " + '\n' +
                    $"{typeSymbol}  {rankSymbol}{typeSymbol} " + '\n';


        var color = card.uncovered ? card.type.GetDisplayColor() : Color.Gray;
        var disabledColor = deckPosition % 2 == 0 ? Color.DarkGray : Color.Gray;

        this.ColorScheme =
            new Terminal.Gui.ColorScheme(
                    new Terminal.Gui.Attribute(color, Color.White),
                    new Terminal.Gui.Attribute(color, Color.BrightYellow),
                    new Terminal.Gui.Attribute(color, Color.White),
                    new Terminal.Gui.Attribute(disabledColor, disabledColor),
                    new Terminal.Gui.Attribute(color, Color.White)
            );

        this.Accepting += (s, e) => OnClick(this);
    }
}


using Terminal.Gui;

namespace Solitare;

public class ReserveView : CardBaseView
{
    public ReserveView(Pos x, Pos y, Action OnClick) : base(x, y, true)
    {
        SetEmpty(false);

        var color = Color.White;

        this.ColorScheme =
            new Terminal.Gui.ColorScheme(
                    new Terminal.Gui.Attribute(color, Color.Gray),
                    new Terminal.Gui.Attribute(Color.Black, Color.Yellow),
                    new Terminal.Gui.Attribute(color, Color.Gray),
                    new Terminal.Gui.Attribute(color, Color.Gray),
                    new Terminal.Gui.Attribute(color, Color.Gray)
            );

        this.Accepting += (s, e) => OnClick();
    }

    public void SetEmpty(bool isEmpty)
    {
        if (isEmpty)
        {
            this.Text = $"       " + '\n' +
                        $" pomie " + '\n' +
                        $" szaj  " + '\n' +
                        $"       " + '\n';
        }
        else
        {
            this.Text = $"       " + '\n' +
                        $" naste " + '\n' +
                        $" pna   " + '\n' +
                        $"       " + '\n';
        }

    }
}


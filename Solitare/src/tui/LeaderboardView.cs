using Terminal.Gui;

namespace Solitare;

/// <summary>
/// The leaderboard view.
/// </summary>
public class LeaderboardView : TableView
{
    public LeaderboardView(Pos x, Pos y)
    {
        this.X = x;
        this.Y = y;
        this.Width = 35;
        this.Height = 20;
        this.Visible = true;
        this.CanFocus = false;
        this.Enabled = true;
        this.ShadowStyle = ShadowStyle.Transparent;
        this.HighlightStyle = HighlightStyle.None;

        this.TextAlignment = Terminal.Gui.Alignment.Start;

        this.Style.AlwaysShowHeaders = true;
        this.Style.ShowHorizontalBottomline = true;

        UpdateTable();
        Leaderboard.instance.OnLeaderboardChange += UpdateTable;
    }

    /// <summary>
    /// Update the table data.
    /// </summary>
    private void UpdateTable()
    {
        this.Table = new DataTableSource(Leaderboard.instance.CreateDataTable());
    }
}


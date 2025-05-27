using Terminal.Gui;

namespace Solitare;

/// <summary>
/// The main view of the game.
/// </summary>
public class GameView : Window
{
    private Game game;
    private DeckViewFinal[] finalDeckViews;
    private DeckViewInitial[] initialDeckViews;
    private DeckViewReserve reserveDeckView;
    private ReserveView reserveNextButton;

    private Label invalidMoveLabel;
    private MenuBarv2 menu;
    private Shortcut undoShortcut;
    private Label moveCount;

    private DeckView? selectedDeck = null;
    private CardView? selectedCard = null;

    private int invalidMoveCount = 0;

#pragma warning disable CS8618
    public GameView(Game game)
#pragma warning restore CS8618
    {
        this.game = game;

        this.Width = Dim.Fill(0);
        this.Height = Dim.Fill(0);
        this.X = 0;
        this.Y = 0;
        this.Visible = true;
        this.Arrangement = Terminal.Gui.ViewArrangement.Overlapped;
        this.CanFocus = true;
        this.ShadowStyle = Terminal.Gui.ShadowStyle.None;
        this.Modal = false;
        this.TextAlignment = Terminal.Gui.Alignment.Start;
        this.Title = "Pasjans gigathon 2025";

        this.ColorScheme = new Terminal.Gui.ColorScheme(
            new Terminal.Gui.Attribute(Color.Black, Color.Green),
            new Terminal.Gui.Attribute(Color.Black, Color.BrightGreen),
            new Terminal.Gui.Attribute(Color.Black, Color.Green),
            new Terminal.Gui.Attribute(Color.Black, Color.Green),
            new Terminal.Gui.Attribute(Color.Black, Color.Green)
        );

        FullRedraw();

        game.OnDeckChange += (deck) =>
        {
            UpdateUndoShortcutText(game.GetStateHistoryLength());
            UpdateMoveCountText(game.state.moveCount);
        };

        game.OnGameWon += () =>
        {
            MessageBox.Query(50, 7, "Wygrałeś!", $"Liczba ruchów: {game.state.moveCount}", "Wyjdź do menu głównego");
            Application.RequestStop();
        };
    }

    public void FullRedraw()
    {
        this.RemoveAll();

        InitializeInvalidMoveLabel();
        InitializeMenu();

        var initialDeckY = 4;
        initialDeckViews = game.state.initialDecks.Select((deck, i) =>
        {
            var deckView = new DeckViewInitial(
                deck,
                Pos.Absolute(3 + (i * (CardView.WIDTH + 1))),
                Pos.Absolute(initialDeckY),
                game,
                OnDeckViewClick
            );
            this.Add(deckView);

            Key[] keyMap = { Key.Q, Key.W, Key.E, Key.R, Key.T, Key.Y, Key.U, Key.I, };
            AttachShortcutToDeckView(deckView, keyMap[i]);

            return deckView;
        }
        ).ToArray();

        var finalDeckY = initialDeckY + CardView.HEIGHT + 14 + 3;
        finalDeckViews = game.state.finalDecks.Select((deck, i) =>
        {
            var deckView = new DeckViewFinal(
                deck,
                Pos.Absolute(3 + (i * (CardView.WIDTH + 1))),
                Pos.Absolute(finalDeckY),
                game,
                OnDeckViewClick
            );
            this.Add(deckView);

            Key[] keyMap = { Key.A, Key.S, Key.D, Key.F, };
            AttachShortcutToDeckView(deckView, keyMap[i]);

            return deckView;
        }
        ).ToArray();

        var reserveNextX = 50;
        reserveNextButton = new ReserveView(reserveNextX, finalDeckY, game.NextReserveCard);
        this.Add(reserveNextButton);
        AttachShortcutToView(reserveNextButton, Key.G, () => { });

        reserveDeckView = new DeckViewReserve(
            game.state.reserveDeck,
            Pos.Absolute(reserveNextX + CardView.WIDTH + 1),
            Pos.Absolute(finalDeckY),
            game,
            OnDeckViewClick,
            reserveNextButton
        );
        this.Add(reserveDeckView);
        AttachShortcutToDeckView(reserveDeckView, Key.H);
    }

    public void UpdateUndoShortcutText(int moves)
    {
        undoShortcut.Title = $"Cofnij ruch ({moves}/{Game.MAX_HISTORY_SIZE})";
    }

    public void UpdateMoveCountText(int moveCount)
    {
        this.moveCount.Title = $"Liczba ruchów: {moveCount}";
    }

    public async void SetInvalidMoveMessage(string error)
    {
        this.invalidMoveLabel.Title = $" Zły ruch: {error}";
        var currentInvalidMoveCount = ++invalidMoveCount;
        await Task.Delay(5000);
        if (currentInvalidMoveCount == invalidMoveCount)
        {
            this.invalidMoveLabel.Title = "";
        }
    }

    private void AttachShortcutToView(View view, Key key, Action action)
    {
        var shourtcut = new Shortcut
        {
            Title = "",
            Key = key,
            HighlightStyle = HighlightStyle.Hover,
            X = view.X,
            Y = view.Y - 1,
            CanFocus = false,
            Action = () =>
            {
                action();
                view.InvokeCommand(Command.Select);
                view.InvokeCommand(Command.Accept);
            },
        };
        this.Add(shourtcut);
    }

    private void AttachShortcutToDeckView(DeckView deckView, Key key)
    {
        AttachShortcutToView(deckView, key, () =>
        {
            var card = deckView.IsEmpty() ? null : deckView.TopCardView();
            if (card != null) card.SetFocus();
            OnDeckViewClick(deckView, card);
        });
    }

    private void OnDeckViewClick(DeckView deckView, CardView? cardView)
    {
        if (selectedCard == null)
        {
            selectedCard = cardView;
        }

        if (selectedDeck == null)
        {
            if (!deckView.IsEmpty()) selectedDeck = deckView;
        }
        else if (selectedCard != null)
        {
            if (selectedDeck == deckView)
            {
                selectedCard = cardView;
            }
            else
            {
                selectedDeck.ClearFocus();

                var fromDeck = selectedDeck.deck;
                var toDeck = deckView.deck;

                var result = game.TryMoveCard(fromDeck, toDeck, selectedCard.card);

                if (result.IsFailed)
                {
                    var error = result.Errors[0].Message;
                    SetInvalidMoveMessage(error);
                }

                selectedDeck?.ClearFocus();
                selectedCard = null;
                selectedDeck = null;
            }
        }
    }

    private void InitializeInvalidMoveLabel()
    {
        this.invalidMoveLabel = new Terminal.Gui.Label()
        {
            X = Pos.Absolute(0),
            Y = Pos.Absolute(1),
            CanFocus = false,
            Visible = true,
            TextAlignment = Terminal.Gui.Alignment.Start,
            Title = "",
            ColorScheme = new Terminal.Gui.ColorScheme(new Terminal.Gui.Attribute(Color.Red, Color.White)),
        };
        this.Add(this.invalidMoveLabel);
    }

    private void InitializeMenu()
    {
        menu = new Terminal.Gui.MenuBarv2
        {
            ColorScheme = new Terminal.Gui.ColorScheme(
                new Terminal.Gui.Attribute(Color.Black, Color.BrightGreen)
            )
        };
        menu.Add(new Shortcut()
        {
            Title = "Wyjdź do menu głównego",
            Key = Key.Esc,
            HighlightStyle = HighlightStyle.Hover,
            Action = () => Application.RequestStop()
        });

        menu.Add(new Label { Title = " | ", CanFocus = false, });

        undoShortcut = new Shortcut
        {
            Key = Key.Z,
            HighlightStyle = HighlightStyle.Hover,
            Action = () =>
            {
                game.UndoMove();
                FullRedraw();
            }
        };
        UpdateUndoShortcutText(0);
        menu.Add(undoShortcut);

        menu.Add(new Label { Title = " | ", CanFocus = false, });

        moveCount = new Label();
        UpdateMoveCountText(0);
        menu.Add(moveCount);

        this.Add(menu);
    }
}


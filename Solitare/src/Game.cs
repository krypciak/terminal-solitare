using FluentResults;
namespace Solitare;

/// <summary>
/// Manages <c>GameState</c>.
/// </summary>
public class Game
{
    /// <value>
    /// How many undo moves are allowed.
    /// </value>
    public const int MAX_HISTORY_SIZE = 3;

    /// <value>
    /// The game state.
    /// </value>
    public GameState state { get; private set; }

    /// <value>
    /// List of JSON serialized <c>GameState</c>.
    /// It can have a maximum size of <c>MAX_HISTORY_SIZE</c>
    /// </value>
    private LinkedList<string> stateHistory = [];

    /// <value>
    /// Difficulty of the game.
    /// </value>
    public Difficulty difficulty { get; }

    /// <value>
    /// Amount of moves that can be undone.
    /// </value>
    public int GetStateHistoryLength() => stateHistory.Count;

    /// <value>
    /// Amount of cards to show in the reserve deck.
    /// Depends on the difficulty.
    /// </value>
    public int reserveShowCount { get; }

    /// <value>
    /// Event that runs when the deck changes, for each deck.
    /// </value>
    public event Action<Deck>? OnDeckChange;

    /// <value>
    /// Event that runs when the game is finished.
    /// </value>
    public event Action? OnGameWon;

    /// <summary>
    /// Creates a new game with an initial layout of cards.
    /// </summary>
    /// <param name="seed">Random seed. Same seeds result in the same initial card configurations.</param>
    /// <param name="difficulty">Difficulty of the game.</param>
    public Game(int seed, Difficulty difficulty)
    {
        this.difficulty = difficulty;
        reserveShowCount = difficulty == Difficulty.Easy ? 1 : 3;

        DeckFinal[] finalDecks = [
             new DeckFinal([]),
                 new DeckFinal([]),
                 new DeckFinal([]),
                 new DeckFinal([])
        ];

        var allCards = GetFullCardList();

        allCards = ShuffleCards(allCards, new Random(seed));

        var allCardsI = 0;
        var initialDecks = new DeckInitial[7];
        for (int i = 0; i < 7; i++)
        {
            List<Card> cards = new List<Card>(i + 1);
            for (int j = 0; j < i; j++)
            {
                var card = allCards[allCardsI++];
                card.uncovered = false;
                cards.Add(card);
            }
            cards.Add(allCards[allCardsI++]);

            initialDecks[i] = new DeckInitial(cards);
        }

        var reserveDeck = new DeckReserve(allCards.Skip(allCardsI).ToList(), reserveShowCount);

        // {
        //     foreach (CardType type in Enum.GetValues(typeof(CardType)))
        //     {
        //         foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
        //         {
        //             finalDecks[(int)type].PushCards([new Card(type, rank)]);
        //         }
        //     }
        //     finalDecks[3].PopCards(1);
        // }

        state = new GameState(finalDecks, initialDecks, reserveDeck, 0);

        Leaderboard.instance.Register(this);
    }

    /// <summary>
    /// Checks if the game is won.
    /// </summary>
    public bool IsGameFinished()
    {
        return state.finalDecks[0].cards.Count == 13
            && state.finalDecks[1].cards.Count == 13
            && state.finalDecks[2].cards.Count == 13
            && state.finalDecks[3].cards.Count == 13;
    }

    /// <summary>
    /// Attempt to move the card.
    /// </summary>
    /// <param name="fromDeck">From which deck to move <paramref name="card"/> from.</param>
    /// <param name="toDeck">To what deck to move <paramref name="card"/> to.</param>
    /// <param name="card">The card to move.</param>
    /// <returns>Result if the operation succeeded. If it failed, the result contains the cause.</returns>
    public Result TryMoveCard(Deck fromDeck, Deck toDeck, Card card)
    {
        var result = toDeck.CanMoveCardHere(card);
        if (result.IsFailed) return result;

        var fromDeckCardIndex = fromDeck.cards.IndexOf(card);
        var cardsToMove = fromDeck.cards.GetRange(fromDeckCardIndex, fromDeck.cards.Count - fromDeckCardIndex);
        if (cardsToMove.Count == 0) return Result.Fail("");

        CommitMove();

        fromDeck.PopCards(cardsToMove.Count);
        toDeck.PushCards(cardsToMove);

        OnDeckChange?.Invoke(fromDeck);
        OnDeckChange?.Invoke(toDeck);

        if (IsGameFinished())
        {
            OnGameWon?.Invoke();
        }

        return result;
    }

    /// <summary>
    /// Attempt to undo a move.
    /// </summary>
    /// <returns><c>true</c> if succeeded, <c>false</c> if there's no move to undo.</returns>
    public bool UndoMove()
    {
        if (stateHistory.Count() == 0) return false;

        var lastStateJson = stateHistory.Last();
        stateHistory.RemoveLast();
        state = GameState.FromJSON(lastStateJson);

        return true;
    }

    /// <summary>
    /// Show the next reserve card.
    /// If the deck is empty, reshuffle it.
    /// </summary>
    public void NextReserveCard()
    {
        CommitMove();
        state.reserveDeck.NextCard();
        OnDeckChange?.Invoke(state.reserveDeck);
    }

    /// <summary>
    /// Increment move count and save the state to history.
    /// </summary>
    private void CommitMove()
    {
        if (stateHistory.Count == Game.MAX_HISTORY_SIZE) stateHistory.RemoveFirst();
        stateHistory.AddLast(state.SerializeToJSON());
        state.moveCount++;
    }

    /// <summary>
    /// Shuffle the list of cards given a an random instance.
    /// </summary>
    /// <param name="cards">Cards to shuffle.</param>
    /// <param name="random">Random instance to use.</param>
    /// <returns>Shuffled list of cards.</returns>
    public static List<Card> ShuffleCards(List<Card> cards, Random random)
    {
        var arr = cards.ToArray();
        random.Shuffle(arr);
        return arr.ToList();
    }

    /// <summary>
    /// Creates a full deck of cards for solitare games.
    /// </summary>
    /// <returns>Full deck of cards.</returns>
    private static List<Card> GetFullCardList()
    {
        List<Card> cards = new(52);

        foreach (CardType type in Enum.GetValues(typeof(CardType)))
        {
            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                cards.Add(new Card(type, rank));
            }
        }

        return cards;
    }
}

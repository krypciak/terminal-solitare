using FluentResults;

namespace Solitare
{
    public class Game
    {
        public const int MAX_HISTORY_SIZE = 3;

        public GameState state { get; private set; }
        private LinkedList<string> stateHistory = [];

        public Difficulty difficulty { get; }

        public int GetStateHistoryLength() => stateHistory.Count;
        public int reserveShowCount { get; }

        public event Action<Deck>? OnDeckChange;
        public event Action? OnGameWon;

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

        public bool IsGameFinished()
        {
            return state.finalDecks[0].cards.Count == 13
                && state.finalDecks[1].cards.Count == 13
                && state.finalDecks[2].cards.Count == 13
                && state.finalDecks[3].cards.Count == 13;
        }

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

        public bool UndoMove()
        {
            if (stateHistory.Count() == 0) return false;

            var lastStateJson = stateHistory.Last();
            stateHistory.RemoveLast();
            state = GameState.FromJSON(lastStateJson);

            return true;
        }

        public void NextReserveCard()
        {
            CommitMove();
            state.reserveDeck.NextCard();
            OnDeckChange?.Invoke(state.reserveDeck);
        }

        private void CommitMove()
        {
            if (stateHistory.Count == Game.MAX_HISTORY_SIZE) stateHistory.RemoveFirst();
            stateHistory.AddLast(state.SerializeToJSON());
            state.moveCount++;
        }

        public static List<Card> ShuffleCards(List<Card> cards, Random random)
        {
            var arr = cards.ToArray();
            random.Shuffle(arr);
            return arr.ToList();
        }

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
}

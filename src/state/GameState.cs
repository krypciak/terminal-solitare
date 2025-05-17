
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentResults;

namespace solitare
{
    public class GameState
    {
        public static int historySize = 3;

        [JsonIgnore]
        public List<string> stateHistory;

        [JsonInclude]
        public DeckFinal[] finalDecks { get; private set; }

        [JsonInclude]
        public DeckInitial[] initialDecks { get; private set; }

        [JsonInclude]
        public DeckReserve reserveDeck { get; private set; }

        [JsonInclude]
        public Difficulty difficulty { get; private set; }

        [JsonInclude]
        private int seed;

        [JsonIgnore]
        private Random random;

        [JsonInclude]
        public int moveCount;

        [JsonConstructor]
        public GameState(int seed, Difficulty difficulty, DeckFinal[] finalDecks, DeckInitial[] initialDecks, DeckReserve reserveDeck, int moveCount)
        {
            this.seed = seed;
            this.difficulty = difficulty;
            this.finalDecks = finalDecks;
            this.initialDecks = initialDecks;
            this.reserveDeck = reserveDeck;
            this.moveCount = moveCount;
            this.stateHistory = [];
            random = new Random(seed);
        }

        public GameState(int seed, Difficulty difficulty)
        {
            this.seed = seed;
            this.difficulty = difficulty;

            finalDecks = [
                 new DeckFinal([]),
                 new DeckFinal([]),
                 new DeckFinal([]),
                 new DeckFinal([])
            ];

            var allCards = GetFullCardList();

            random = new Random(seed);
            allCards = ShuffleCards(allCards);

            var allCardsI = 0;
            initialDecks = new DeckInitial[7];
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

                this.initialDecks[i] = new DeckInitial(cards);
            }

            reserveDeck = new DeckReserve(allCards.Skip(allCardsI).ToList());

            stateHistory = [];
            moveCount = 0;
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

        public List<Card> ShuffleCards(List<Card> cards)
        {
            var arr = cards.ToArray();
            random.Shuffle(arr);
            return arr.ToList();
        }

        public bool IsGameFinished()
        {
            return finalDecks[0].cards.Count == 13
                && finalDecks[1].cards.Count == 13
                && finalDecks[2].cards.Count == 13
                && finalDecks[3].cards.Count == 13;
        }


        public void CommitMove()
        {
            if (this.stateHistory.Count == GameState.historySize) this.stateHistory.RemoveAt(0);
            this.stateHistory.Add(this.SerializeToJSON());
            moveCount++;
        }

        public Result TryMoveCard(Card selCard, Deck selDeck, Deck toDeck)
        {
            var result = toDeck.CanMoveCardHere(selCard);
            if (result.IsFailed) return result;

            var indexFrom = GameView.selectedCard.deckPosition;
            var indexTo = selDeck.cards.Count;

            var cardsToMove = selDeck.cards.GetRange(indexFrom, indexTo - indexFrom);
            if (cardsToMove.Count == 0) return Result.Fail("");

            CommitMove();

            selDeck.PopCards(cardsToMove.Count);
            toDeck.PushCards(cardsToMove);

            return result;
        }

        public bool UndoMove()
        {
            if (stateHistory.Count() == 0) return false;
            var lastStateJson = stateHistory.Last();
            stateHistory.RemoveAt(stateHistory.Count - 1);
            var lastGameState = GameState.FromJSON(lastStateJson);

            this.moveCount = lastGameState.moveCount;
            this.finalDecks = lastGameState.finalDecks;
            this.initialDecks = lastGameState.initialDecks;
            this.reserveDeck = lastGameState.reserveDeck;

            return true;
        }

        private string SerializeToJSON()
        {
            return JsonSerializer.Serialize(this, typeof(GameState), new JsonSerializerOptions
            {
                // WriteIndented = true,
                IncludeFields = true,
            });
        }

        private static GameState FromJSON(string json)
        {
            return JsonSerializer.Deserialize<GameState>(json)!;
        }
    }
}

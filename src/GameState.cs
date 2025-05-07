
using System.Text.Json.Serialization;

namespace solitare
{
    public class GameState
    {
        public DeckFinal[] finalDecks { get; private set; }
        [JsonInclude]

        public DeckInitial[] initialDecks { get; private set; }

        [JsonInclude]
        public DeckReserve reserveDeck { get; private set; }

        [JsonInclude]
        public Difficulty difficulty;

        [JsonInclude]
        private int seed;

        [JsonIgnore]
        private Random random;

        [JsonConstructor]
        public GameState(int seed, Difficulty difficulty, DeckFinal[] finalDecks, DeckInitial[] initialDecks, DeckReserve reserveDeck)
        {
            this.seed = seed;
            this.difficulty = difficulty;
            this.finalDecks = finalDecks;
            this.initialDecks = initialDecks;
            this.reserveDeck = reserveDeck;
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
        }

        private static List<Card> GetFullCardList()
        {
            Card[] cards = new Card[52];

            var i = 0;
            for (CardType type = CardType.Kier; type <= CardType.Trefl; type++)
            {
                for (CardRank rank = CardRank.As; rank <= CardRank.Krol; rank++)
                {
                    cards[i++] = new Card(type, rank);
                }
            }
            return cards.ToList();
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
    }

}


namespace solitare
{
    public class GameState
    {
        public DeckFinal[] finalDecks { get; private set; }
        public DeckInitial[] initialDecks { get; private set; }
        public DeckReserve reserveDeck { get; private set; }
        public Difficulty difficulty;

        private Random random;

        public GameState(int seed, Difficulty difficulty)
        {
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
                Card[] cards = new Card[i + 1];
                for (int j = 0; j < i; j++)
                {
                    cards[j] = allCards[allCardsI++];
                }
                cards[i] = allCards[allCardsI++];

                this.initialDecks[i] = new DeckInitial(cards.ToList());
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
    }

}

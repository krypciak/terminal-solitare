
namespace myproj
{
    public class GameState
    {
        public DeckFinal[] finalDecks { get; private set; }
        public DeckInitial[] initialDecks { get; private set; }
        public DeckReserve reserveDeck { get; private set; }

        public GameState(int seed)
        {
            finalDecks = [
                new DeckFinal([], CardType.Karo),
                 new DeckFinal([], CardType.Kier),
                 new DeckFinal([], CardType.Pik),
                 new DeckFinal([], CardType.Trefl)
            ];


            var allCards = GetFullCardList();
            (new Random(seed)).Shuffle(allCards);

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

        private static Card[] GetFullCardList()
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
            return cards;
        }
    }

}

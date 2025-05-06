namespace myproj
{
    public class GameState
    {
        public DeckFinal[] finalDecks { get; private set; }
        public DeckInitial[] initialDecks { get; private set; }
        public DeckReserve reserveDeck { get; private set; }

        public GameState()
        {
            finalDecks = [
                new DeckFinal([], CardType.Karo),
                 new DeckFinal([], CardType.Kier),
                 new DeckFinal([], CardType.Pik),
                 new DeckFinal([], CardType.Trefl)
            ];

            initialDecks = new DeckInitial[7];
            for (int i = 0; i < 7; i++)
            {
                Card[] cards = new Card[i + 1];
                for (int j = 0; j < i; j++)
                {
                    cards[j] = new Card(CardType.Trefl, CardRank.As);
                }
                cards[i] = new Card(CardType.Karo, CardRank.Dama);

                this.initialDecks[i] = (new DeckInitial(cards));
            }

            reserveDeck = new DeckReserve([new Card(CardType.Karo, CardRank.K4)]);
        }
    }
}

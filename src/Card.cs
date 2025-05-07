using System.Text.Json.Serialization;

namespace solitare
{
    public class Card
    {
        [JsonInclude]
        public CardType type { get; private set; }
        [JsonInclude]
        public CardRank rank { get; private set; }
        [JsonInclude]
        public bool uncovered = true;

        public Card(CardType type, CardRank rank)
        {
            this.type = type;
            this.rank = rank;
        }

        public bool isCardRed()
        {
            return type == CardType.Karo || type == CardType.Kier;
        }
    }
}

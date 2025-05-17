namespace solitare
{
    public enum CardType
    {
        Kier = 0,
        Karo = 1,
        Pik = 2,
        Trefl = 3,
    }
    public static class CardTypeExtensions
    {
        public static bool IsRed(this CardType type) => type is CardType.Karo or CardType.Kier;
    }
}

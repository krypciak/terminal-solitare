using Terminal.Gui;

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

        public static char GetAsciiSymbol(this CardType type) => type switch
        {
            CardType.Karo => '♦',
            CardType.Kier => '♥',
            CardType.Pik => '♠',
            CardType.Trefl => '♣',
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };



        public static Color GetDisplayColor(this CardType type) => type switch
        {
            CardType.Karo or CardType.Kier => Color.Red,
            CardType.Pik or CardType.Trefl => Color.Black,
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
    }
}

namespace Solitare;

public enum CardRank
{
    Krol = 13,
    Dama = 12,
    Walet = 11,
    K10 = 10,
    K9 = 9,
    K8 = 8,
    K7 = 7,
    K6 = 6,
    K5 = 5,
    K4 = 4,
    K3 = 3,
    K2 = 2,
    As = 1,
}

public static class CardRankExtensions
{
    public static string GetAsciiText(this CardRank rank) => rank switch
    {
        CardRank.Krol => "K ",
        CardRank.Dama => "Q ",
        CardRank.Walet => "J ",
        CardRank.K10 => "10",
        CardRank.K9 => "9 ",
        CardRank.K8 => "8 ",
        CardRank.K7 => "7 ",
        CardRank.K6 => "6 ",
        CardRank.K5 => "5 ",
        CardRank.K4 => "4 ",
        CardRank.K3 => "3 ",
        CardRank.K2 => "2 ",
        CardRank.As => "A ",
        _ => throw new ArgumentOutOfRangeException(nameof(rank)),
    };
}

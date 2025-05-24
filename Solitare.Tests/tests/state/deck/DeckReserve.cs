namespace Solitare.Tests;

[TestClass]
public sealed class DeckReserveTest
{
    [TestMethod]
    public void NextShow1()
    {
        var deck = new DeckReserve([
            new Card(CardType.Karo, CardRank.K10),
            new Card(CardType.Trefl, CardRank.K5)
        ], 1);

        deck.NextCard();
        Assert.AreEqual(deck.cards.Count, 1);

        deck.NextCard();
        Assert.AreEqual(deck.cards.Count, 0);

        deck.NextCard();
        Assert.AreEqual(deck.cards.Count, 2);
    }

    [TestMethod]
    public void NextShow3()
    {
        var deck = new DeckReserve([
            new Card(CardType.Karo, CardRank.K10),
            new Card(CardType.Trefl, CardRank.K5),
            new Card(CardType.Pik, CardRank.K7),
            new Card(CardType.Pik, CardRank.K7),
        ], 3);

        deck.NextCard();
        Assert.AreEqual(deck.cards.Count, 1);

        deck.NextCard();
        Assert.AreEqual(deck.cards.Count, 0);

        deck.NextCard();
        Assert.AreEqual(deck.cards.Count, 4);
    }
}

using FluentResults;

namespace Solitare.Tests;

[TestClass]
public sealed class DeckFinalTest
{
    [TestMethod]
    [DataRow(CardType.Karo, CardRank.As)]
    [DataRow(CardType.Pik, CardRank.As)]
    [DataRow(CardType.Karo, CardRank.As)]
    [DataRow(CardType.Trefl, CardRank.As)]
    public void CanMoveCardHereOnEmptySuccess(CardType type, CardRank rank)
    {
        // Arrange
        var deck = new DeckFinal([]);
        var cardToCheck = new Card(type, rank);

        // Act
        Func<Result> func = () => deck.CanMoveCardHere(cardToCheck);

        // Assert
        Assert.IsTrue(func().IsSuccess);
    }

    [TestMethod]
    [DataRow(CardType.Karo, CardRank.K2)]
    [DataRow(CardType.Pik, CardRank.K6)]
    [DataRow(CardType.Karo, CardRank.Krol)]
    [DataRow(CardType.Trefl, CardRank.K2)]
    public void CanMoveCardHereOnEmptyFail(CardType type, CardRank rank)
    {
        // Arrange
        var deck = new DeckFinal([]);
        var cardToCheck = new Card(type, rank);

        // Act
        Func<Result> func = () => deck.CanMoveCardHere(cardToCheck);

        // Assert
        Assert.IsTrue(func().IsFailed);
    }

    [TestMethod]
    [DataRow(CardType.Trefl, CardRank.K2, CardType.Trefl, CardRank.K3)]
    [DataRow(CardType.Pik, CardRank.As, CardType.Pik, CardRank.K2)]
    [DataRow(CardType.Karo, CardRank.Dama, CardType.Karo, CardRank.Krol)]
    public void CanMoveCardHereOnNonEmptySuccess(CardType onDeckType, CardRank onDeckRank, CardType type, CardRank rank)
    {
        // Arrange
        var deck = new DeckFinal([new Card(onDeckType, onDeckRank)]);
        var cardToCheck = new Card(type, rank);

        // Act
        Func<Result> func = () => deck.CanMoveCardHere(cardToCheck);

        // Assert
        Assert.IsTrue(func().IsSuccess);
    }

    [TestMethod]
    [DataRow(CardType.Trefl, CardRank.K2, CardType.Karo, CardRank.K3)]
    [DataRow(CardType.Pik, CardRank.As, CardType.Pik, CardRank.K3)]
    [DataRow(CardType.Karo, CardRank.Dama, CardType.Karo, CardRank.Walet)]
    public void CanMoveCardHereOnNonEmptyFail(CardType onDeckType, CardRank onDeckRank, CardType type, CardRank rank)
    {
        // Arrange
        var deck = new DeckFinal([new Card(onDeckType, onDeckRank)]);
        var cardToCheck = new Card(type, rank);

        // Act
        Func<Result> func = () => deck.CanMoveCardHere(cardToCheck);

        // Assert
        Assert.IsTrue(func().IsFailed);
    }
}

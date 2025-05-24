using FluentResults;

namespace Solitare.Tests;

[TestClass]
public sealed class DeckInitialTest
{
    [TestMethod]
    [DataRow(CardType.Karo, CardRank.Krol)]
    [DataRow(CardType.Pik, CardRank.Krol)]
    [DataRow(CardType.Karo, CardRank.Krol)]
    [DataRow(CardType.Trefl, CardRank.Krol)]
    public void CanMoveCardHereOnEmptySuccess(CardType type, CardRank rank)
    {
        // Arrange
        var deck = new DeckInitial([]);
        var cardToCheck = new Card(type, rank);

        // Act
        Func<Result> func = () => deck.CanMoveCardHere(cardToCheck);

        // Assert
        Assert.IsTrue(func().IsSuccess);
    }

    [TestMethod]
    [DataRow(CardType.Karo, CardRank.K2)]
    [DataRow(CardType.Pik, CardRank.K6)]
    [DataRow(CardType.Karo, CardRank.As)]
    [DataRow(CardType.Trefl, CardRank.K2)]
    public void CanMoveCardHereOnEmptyFail(CardType type, CardRank rank)
    {
        // Arrange
        var deck = new DeckInitial([]);
        var cardToCheck = new Card(type, rank);

        // Act
        Func<Result> func = () => deck.CanMoveCardHere(cardToCheck);

        // Assert
        Assert.IsTrue(func().IsFailed);
    }

    [TestMethod]
    [DataRow(CardType.Trefl, CardRank.K2, CardType.Karo, CardRank.As)]
    [DataRow(CardType.Trefl, CardRank.K2, CardType.Kier, CardRank.As)]
    [DataRow(CardType.Pik, CardRank.Krol, CardType.Karo, CardRank.Dama)]
    [DataRow(CardType.Kier, CardRank.K9, CardType.Trefl, CardRank.K8)]
    public void CanMoveCardHereOnNonEmptySuccess(CardType onDeckType, CardRank onDeckRank, CardType type, CardRank rank)
    {
        // Arrange
        var deck = new DeckInitial([new Card(onDeckType, onDeckRank)]);
        var cardToCheck = new Card(type, rank);

        // Act
        Func<Result> func = () => deck.CanMoveCardHere(cardToCheck);

        // Assert
        Assert.IsTrue(func().IsSuccess);
    }

    [TestMethod]
    [DataRow(CardType.Trefl, CardRank.K2, CardType.Trefl, CardRank.As)]
    [DataRow(CardType.Trefl, CardRank.K2, CardType.Pik, CardRank.As)]
    [DataRow(CardType.Pik, CardRank.As, CardType.Karo, CardRank.K3)]
    [DataRow(CardType.Karo, CardRank.K7, CardType.Kier, CardRank.K6)]
    public void CanMoveCardHereOnNonEmptyFail(CardType onDeckType, CardRank onDeckRank, CardType type, CardRank rank)
    {
        // Arrange
        var deck = new DeckInitial([new Card(onDeckType, onDeckRank)]);
        var cardToCheck = new Card(type, rank);

        // Act
        Func<Result> func = () => deck.CanMoveCardHere(cardToCheck);

        // Assert
        Assert.IsTrue(func().IsFailed);
    }
}

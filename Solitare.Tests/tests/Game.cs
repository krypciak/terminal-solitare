using FluentResults;

namespace Solitare.Tests;

[TestClass]
public sealed class GameTest
{
    [TestMethod]
    public void TryMoveCard()
    {
        // Arrange
        var game = new Game(123, Difficulty.Easy);

        var card = new Card(CardType.Trefl, CardRank.Dama);

        var fromDeck = game.state.initialDecks[0];
        fromDeck.PopCards(fromDeck.cards.Count);
        fromDeck.PushCards([new Card(CardType.Karo, CardRank.Krol), card, new Card(CardType.Karo, CardRank.Walet)]);

        var toDeck = game.state.initialDecks[1];
        toDeck.PopCards(toDeck.cards.Count);
        toDeck.PushCards([new Card(CardType.Karo, CardRank.Krol)]);

        // Act
        Func<Result> func = () => game.TryMoveCard(fromDeck, toDeck, card);

        // Assert
        Assert.IsTrue(func().IsSuccess);
        Assert.AreEqual(fromDeck.cards.Count, 1);
        Assert.AreEqual(toDeck.cards.Count, 3);
        Assert.AreEqual(game.state.moveCount, 1);
    }

    [TestMethod]
    public void UndoMove()
    {
        // Arrange
        var game = new Game(123, Difficulty.Easy);

        var card = new Card(CardType.Trefl, CardRank.Dama);

        var fromDeck = () => game.state.initialDecks[0];
        fromDeck().PopCards(fromDeck().cards.Count);
        fromDeck().PushCards([new Card(CardType.Karo, CardRank.Krol), card, new Card(CardType.Karo, CardRank.Walet)]);

        var toDeck = () => game.state.initialDecks[1];
        toDeck().PopCards(toDeck().cards.Count);
        toDeck().PushCards([new Card(CardType.Karo, CardRank.Krol)]);

        // Act
        Func<Result> move1 = () => game.TryMoveCard(fromDeck(), toDeck(), card);

        // Assert
        Assert.IsFalse(game.UndoMove());

        Assert.IsTrue(move1().IsSuccess);
        Assert.AreEqual(fromDeck().cards.Count, 1);
        Assert.AreEqual(toDeck().cards.Count, 3);
        Assert.AreEqual(game.state.moveCount, 1);

        Assert.IsTrue(game.UndoMove());
        Assert.AreEqual(fromDeck().cards.Count, 3);
        Assert.AreEqual(toDeck().cards.Count, 1);
        Assert.AreEqual(game.state.moveCount, 0);
    }
}

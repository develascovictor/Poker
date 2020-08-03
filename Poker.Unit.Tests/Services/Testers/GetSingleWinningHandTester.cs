using NUnit.Framework;
using Poker.Models;
using Poker.Unit.Tests.Services.Testers.Base;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Unit.Tests.Services.Testers
{
    public class GetSingleWinningHandTester : BaseGetWinningHandTester<Hand>
    {
        protected override void ValidateGetWinningHands(List<Hand> winningHands)
        {
            Assert.IsNotEmpty(winningHands);
            Assert.AreEqual(1, winningHands.Count);

            var singleWinningHand = winningHands.Single();
            Assert.IsNotNull(singleWinningHand);

            var cards = singleWinningHand.GetCards().ToList();
            Assert.IsNotEmpty(cards);
            Assert.AreEqual(5, cards.Count);

            for (var i = 0; i < 5; i++)
            {
                Assert.IsNotNull(cards[i], $"Card is null. [i: {i}]");

                var expectedCards = ExpectedResult.GetCards().ToList();
                Assert.AreEqual(expectedCards[i].Suit, cards[i].Suit, GetIterationError(nameof(Card.Suit), i));
                Assert.AreEqual(expectedCards[i].Value, cards[i].Value, GetIterationError(nameof(Card.Value), i));
            }
        }

        protected override void ValidateExpectedResult()
        {
            Assert.IsNotNull(ExpectedResult);

            var expectedCards = ExpectedResult.GetCards();
            Assert.IsNotEmpty(expectedCards);
            Assert.AreEqual(5, expectedCards.Count);
            Assert.IsTrue(expectedCards.All(x => x != null));
        }

        private string GetIterationError(string concept, int i) => $"\"{concept}\" doesn't match. [i: {i}]";
    }
}
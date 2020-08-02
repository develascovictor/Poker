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
            Assert.IsNotEmpty(singleWinningHand.Cards);
            Assert.AreEqual(5, singleWinningHand.Cards.Count);

            for (var i = 0; i < 5; i++)
            {
                Assert.IsNotNull(singleWinningHand.Cards[i], $"Card is null. [i: {i}]");
                Assert.AreEqual(ExpectedResult.Cards[i].Suit, singleWinningHand.Cards[i].Suit, GetIterationError(nameof(Card.Suit), i));
                Assert.AreEqual(ExpectedResult.Cards[i].Value, singleWinningHand.Cards[i].Value, GetIterationError(nameof(Card.Value), i));
            }
        }

        protected override void ValidateExpectedResult()
        {
            Assert.IsNotNull(ExpectedResult);
            Assert.IsNotEmpty(ExpectedResult.Cards);
            Assert.AreEqual(5, ExpectedResult.Cards.Count);
            Assert.IsTrue(ExpectedResult.Cards.All(x => x != null));
        }

        private string GetIterationError(string concept, int i) => $"\"{concept}\" doesn't match. [i: {i}]";
    }
}
using NUnit.Framework;
using Poker.Models;
using Poker.Services;
using Poker.Services.Base;
using Poker.Unit.Tests.Services.Testers.Base;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Unit.Tests.Services.Testers
{
    public class GetSingleWinningHandTester<TCardGameService> : BaseGetWinningHandTester<Hand, TCardGameService> where TCardGameService : BaseCardGameService
    {
        protected override void ValidateGetWinningHands(List<Hand> winningHands)
        {
            if (_cardGameServiceType == typeof(PokerService))
            {
                Assert.IsNotEmpty(winningHands);
            }

            if (winningHands.Any())
            {
                Assert.AreEqual(1, winningHands.Count);

                var singleWinningHand = winningHands.Single();
                Assert.IsNotNull(singleWinningHand);

                var cards = singleWinningHand.GetCards().ToList();
                Assert.IsNotEmpty(cards);
                ValidateHandSize(cards);

                for (var i = 0; i < cards.Count; i++)
                {
                    Assert.IsNotNull(cards[i], $"Card is null. [i: {i}]");

                    var expectedCards = ExpectedResult.GetCards().ToList();
                    Assert.AreEqual(expectedCards[i].Suit, cards[i].Suit, GetIterationError(nameof(Card.Suit), i));
                    Assert.AreEqual(expectedCards[i].Value, cards[i].Value, GetIterationError(nameof(Card.Value), i));
                }
            }

            else
            {
                // If there are no winning hands, it's because you are expecting a null result
                Assert.IsNull(ExpectedResult);
            }
        }

        protected override void ValidateExpectedResult()
        {
            if (_cardGameServiceType == typeof(PokerService))
            {
                Assert.IsNotNull(ExpectedResult);
            }

            if (ExpectedResult != null)
            {
                var expectedCards = ExpectedResult.GetCards();
                Assert.IsNotEmpty(expectedCards);
                ValidateHandSize(expectedCards);
                Assert.IsTrue(expectedCards.All(x => x != null));
            }
        }

        private string GetIterationError(string concept, int i) => $"\"{concept}\" doesn't match. [i: {i}]";
    }
}
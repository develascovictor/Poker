using NUnit.Framework;
using Poker.Extensions;
using Poker.Models;
using Poker.Services.Interfaces;
using Poker.Unit.Tests.Services.Testers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Unit.Tests.Services.Testers
{
    public class GetSingleWinningHandTester : IGetWinningHandsTester<Hand>
    {
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Test case
        /// </summary>
        public List<Hand> Hands { get; set; }

        /// <summary>
        /// Expected result
        /// </summary>
        public Hand ExpectedResult { get; set; }

        public void RunGetWinningHands(IPokerService pokerService)
        {
            var errorMessage = ErrorMessage();

            try
            {
                ValidateInitialProperties(pokerService);

                var winningHands = pokerService.GetWinningHands(Hands);
                Assert.IsNotEmpty(winningHands, errorMessage);
                Assert.AreEqual(1, winningHands.Count, errorMessage);

                var singleWinningHand = winningHands.Single();
                Assert.IsNotNull(singleWinningHand, errorMessage);
                Assert.IsNotEmpty(singleWinningHand.Cards, errorMessage);
                Assert.AreEqual(5, singleWinningHand.Cards.Count, errorMessage);

                for (var i = 0; i < 5; i++)
                {
                    Assert.IsNotNull(singleWinningHand.Cards[i], $"Card is null. [i: {i}]\n\n{errorMessage}");
                    Assert.AreEqual(ExpectedResult.Cards[i].Suit, singleWinningHand.Cards[i].Suit, GetIterationError(nameof(Card.Suit), i));
                    Assert.AreEqual(ExpectedResult.Cards[i].Value, singleWinningHand.Cards[i].Value, GetIterationError(nameof(Card.Value), i));
                }
            }

            catch (Exception e)
            {
                throw new Exception(errorMessage, e);
            }
        }

        private void ValidateInitialProperties(IPokerService pokerService)
        {
            var errorMessage = ErrorMessage();
            Assert.IsNotEmpty(Hands, errorMessage);

            for (var i = 0; i < Hands.Count; i++)
            {
                Assert.IsNotNull(Hands[i], errorMessage);
                Assert.IsNotEmpty(Hands[i].Cards, errorMessage);
                Assert.AreEqual(5, Hands[i].Cards.Count, errorMessage);
                Assert.IsTrue(Hands[i].Cards.All(x => x != null), errorMessage);
            }

            Assert.IsNotNull(ExpectedResult, errorMessage);
            Assert.IsNotEmpty(ExpectedResult.Cards, errorMessage);
            Assert.AreEqual(5, ExpectedResult.Cards.Count, errorMessage);
            Assert.IsTrue(ExpectedResult.Cards.All(x => x != null), errorMessage);

            Assert.IsNotNull(pokerService, errorMessage);
        }

        private string GetIterationError(string concept, int i) => $"{concept} don't match. [i: {i}]\n\n{ErrorMessage()}";

        private string ErrorMessage()
        {
            var testCase = Hands.Stringify();
            var expectedResult = ExpectedResult.Stringify();

            return $"Description: {Description}\n\nTest Case:\n{testCase}\n\nExpected Result:\n{expectedResult}";
        }
    }
}
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
    public class GetMultipleWinningHandsTester : IGetWinningHandsTester<List<Hand>>
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
        public List<Hand> ExpectedResult { get; set; }

        public void RunGetWinningHands(IPokerService pokerService)
        {
            var errorMessage = ErrorMessage();

            try
            {
                ValidateInitialProperties(pokerService);

                var winningHands = pokerService.GetWinningHands(Hands).ToList();
                Assert.IsNotEmpty(winningHands, errorMessage);
                Assert.AreEqual(ExpectedResult.Count, winningHands.Count, errorMessage);

                for (var i = 0; i < ExpectedResult.Count; i++)
                {
                    var winningHand = winningHands[i];
                    Assert.IsNotNull(winningHand, errorMessage);
                    Assert.IsNotEmpty(winningHand.Cards, errorMessage);
                    Assert.AreEqual(5, winningHand.Cards.Count, errorMessage);

                    for (var j = 0; j < 5; j++)
                    {
                        Assert.IsNotNull(winningHand.Cards[j], $"Card is null. [i: {j}] [i: {j}]\n\n{errorMessage}");
                        Assert.AreEqual(ExpectedResult[i].Cards[j].Suit, winningHand.Cards[j].Suit, GetIterationError(nameof(Card.Suit), i, j));
                        Assert.AreEqual(ExpectedResult[i].Cards[j].Value, winningHand.Cards[j].Value, GetIterationError(nameof(Card.Value), i, j));
                    }
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

            Assert.IsNotEmpty(ExpectedResult);

            for (var i = 0; i < ExpectedResult.Count; i++)
            {
                Assert.IsNotNull(ExpectedResult[i], errorMessage);
                Assert.IsNotEmpty(ExpectedResult[i].Cards, errorMessage);
                Assert.AreEqual(5, ExpectedResult[i].Cards.Count, errorMessage);
                Assert.IsTrue(ExpectedResult[i].Cards.All(x => x != null), errorMessage);
            }

            Assert.IsNotNull(pokerService, errorMessage);
        }

        private string GetIterationError(string concept, int i, int j) => $"{concept} don't match. [i: {i}] [i: {j}]\n\n{ErrorMessage()}";

        private string ErrorMessage()
        {
            var testCase = Hands.Stringify();
            var expectedResult = ExpectedResult.Stringify();

            return $"Description: {Description}\n\nTest Case:\n{testCase}\n\nExpected Result:\n{expectedResult}";
        }
    }
}
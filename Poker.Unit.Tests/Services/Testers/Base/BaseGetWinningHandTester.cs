using NUnit.Framework;
using Poker.Models;
using Poker.Services;
using Poker.Services.Base;
using Poker.Services.Interfaces;
using Poker.Unit.Tests.Services.Testers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Unit.Tests.Services.Testers.Base
{
    public abstract class BaseGetWinningHandTester<TExpectedResult, TCardGameService> : IGetWinningHandsTester<TExpectedResult> where TCardGameService : BaseCardGameService
    {
        protected readonly Type _cardGameServiceType = typeof(TCardGameService);

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
        public TExpectedResult ExpectedResult { get; set; }

        protected abstract void ValidateGetWinningHands(List<Hand> winningHands);

        protected abstract void ValidateExpectedResult();

        public void RunGetWinningHands(ICardGameService cardGameService)
        {
            try
            {
                ValidateInitialProperties(cardGameService);
                ValidateExpectedResult();

                var winningHands = cardGameService.GetWinningHands(Hands).ToList();
                ValidateGetWinningHands(winningHands);
            }

            catch
            {
                Console.WriteLine($"Description: {Description}");
                throw;
            }
        }

        private void ValidateInitialProperties(ICardGameService cardGameService)
        {
            Assert.IsNotEmpty(Hands);

            for (var i = 0; i < Hands.Count; i++)
            {
                Assert.IsNotNull(Hands[i]);

                var cards = Hands[i].GetCards();
                Assert.IsNotEmpty(cards);
                ValidateHandSize(cards);
                Assert.IsTrue(cards.All(x => x != null));
            }

            Assert.IsNotNull(cardGameService);
        }

        protected void ValidateHandSize(IReadOnlyCollection<Card> cards, string errorMessage = null)
        {
            if (_cardGameServiceType == typeof(PokerService))
            {
                Assert.AreEqual(5, cards.Count, errorMessage);
            }

            else if (_cardGameServiceType == typeof(BlackJackService))
            {
                Assert.GreaterOrEqual(cards.Count, 2, errorMessage);
            }
        }
    }
}
using NUnit.Framework;
using Poker.Enums;
using Poker.Exceptions;
using Poker.Models;
using Poker.Services;
using Poker.Services.Interfaces;
using Poker.Unit.Tests.Services.Base;
using Poker.Unit.Tests.Services.Testers;
using Poker.Unit.Tests.Services.Testers.Collections;
using Poker.Unit.Tests.Services.Testers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Unit.Tests.Services
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class BlackJackServiceTests : BaseCardGameServiceTests<IBlackJackService>
    {
        [SetUp]
        public void SetUp()
        {
            _initialHandCardCount = 2;
            _cardGameService = new BlackJackService();
        }

        [TestCaseSource(nameof(GetSingleWinningHandTestCases))]
        public override void ShouldGetSingleWinningHand(IGetWinningHandsTester<Hand> tester)
        {
            tester.RunGetWinningHands(_cardGameService);
        }

        [TestCaseSource(nameof(GetMultipleWinningHandTestCases))]
        public override void ShouldGetMultipleWinningHand(IGetWinningHandsTester<List<Hand>> tester)
        {
            tester.RunGetWinningHands(_cardGameService);
        }

        [Test]
        public void ShouldAskForCard()
        {
            var firstHand = _cardGameService.GetHand();
            var updatedHand = _cardGameService.AskForCard(firstHand);

            Assert.IsNotNull(updatedHand);

            var cardsFromFirstHand = firstHand.GetCards().ToList();
            var cardsFromUpdatedHand = updatedHand.GetCards().ToList();

            Assert.IsNotEmpty(cardsFromUpdatedHand);
            Assert.AreEqual(cardsFromFirstHand.Count + 1, cardsFromUpdatedHand.Count);

            for (var i = 0; i < cardsFromFirstHand.Count; i++)
            {
                Assert.AreEqual(cardsFromFirstHand[i].Suit, cardsFromUpdatedHand[i].Suit);
                Assert.AreEqual(cardsFromFirstHand[i].Value, cardsFromUpdatedHand[i].Value);
            }

            var newCard = cardsFromUpdatedHand.Last();
            Assert.IsNotNull(newCard);
            Assert.IsFalse(cardsFromFirstHand.Any(x => x.Value == newCard.Value && x.Suit == newCard.Suit));
        }

        [Test]
        public void ShouldThrowInvalidDrawOnBustHandExceptionOnAskForCard()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Club, 5),
                new Card(Suits.Club, 8),
                new Card(Suits.Club, 10),
            };
            var firstHand = new Hand(cards);
            Assert.Throws<InvalidDrawOnBustHandException>(() => _cardGameService.AskForCard(firstHand));
        }

        private static IEnumerable<IGetWinningHandsTester<Hand>> GetSingleWinningHandTestCases()
        {
            yield return new GetSingleWinningHandTester<BlackJackService>
            {
                Description = "No Winning Hands",
                Hands = new List<Hand>
                {
                    Hands.BlackJack.TwentyFour
                },
                ExpectedResult = null
            };
            yield return new GetSingleWinningHandTester<BlackJackService>
            {
                Description = "Winning Hand - Twenty",
                Hands = new List<Hand>
                {
                    Hands.BlackJack.Sixteen,
                    Hands.BlackJack.Twenty,
                    Hands.BlackJack.TwentyFour,
                    Hands.BlackJack.TwentyTwo,
                },
                ExpectedResult = Hands.BlackJack.Twenty
            };
        }

        private static IEnumerable<IGetWinningHandsTester<List<Hand>>> GetMultipleWinningHandTestCases()
        {
            yield return new GetMultipleWinningHandsTester<BlackJackService>
            {
                Description = "No Winning Hands",
                Hands = new List<Hand>
                {
                    Hands.BlackJack.TwentyFour
                },
                ExpectedResult = new List<Hand>()
            };
            yield return new GetMultipleWinningHandsTester<BlackJackService>
            {
                Description = "Winning Hands - Seventeen",
                Hands = new List<Hand>
                {
                    Hands.BlackJack.Sixteen,
                    Hands.BlackJack.Seventeen1,
                    Hands.BlackJack.Seventeen2,
                    Hands.BlackJack.TwentyTwo
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.BlackJack.Seventeen1,
                    Hands.BlackJack.Seventeen2
                }
            };
        }

        private static IList<Card> FillCards()
        {
            //Instantiate list
            var cards = new List<Card>();

            //Used for iterations
            int i;

            //Obtain list of all suits
            var suits = Enum.GetValues(typeof(Suits)).Cast<Suits>();

            //Fill deck with cards
            foreach (var suit in suits)
            {
                for (i = 2; i <= 14; i++)
                {
                    cards.Add(new Card(suit, i));
                }
            }

            return cards;
        }
    }
}
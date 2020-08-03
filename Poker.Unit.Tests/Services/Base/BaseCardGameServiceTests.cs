using NUnit.Framework;
using Poker.Enums;
using Poker.Exceptions;
using Poker.Models;
using Poker.Services.Interfaces;
using Poker.Unit.Tests.Services.Testers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Unit.Tests.Services.Base
{
    public abstract class BaseCardGameServiceTests
    {
        protected ICardGameService _cardGameService;
        protected int _initialHandCardCount;

        [Test]
        public void ShouldGetHand()
        {
            var hand = _cardGameService.GetHand();
            Assert.IsNotNull(hand);

            var cards = hand.GetCards();
            Assert.IsNotEmpty(cards);
            Assert.AreEqual(_initialHandCardCount, cards.Count);
            Assert.IsTrue(cards.All(x => x != null));
            Assert.IsTrue(cards.GroupBy(x => new { x.Suit, x.Value }).All(x => x.Count() == 1));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ShouldThrowMissingWinningHandsExceptionOnGetWinningHands(bool isNull)
        {
            Assert.Throws<MissingWinningHandsException>(() => _cardGameService.GetWinningHands(isNull ? null : new List<Hand>()));
        }

        [Test]
        public void ShouldThrowHandInWinningHandsNullExceptionOnGetWinningHands()
        {
            Assert.Throws<HandInWinningHandsNullException>(() => _cardGameService.GetWinningHands(new List<Hand> { null, new Hand(null) }));
        }

        public void ShouldThrowCardInPokerHandIsNullExceptionOnGetWinningHands()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Club, 2),
                new Card(Suits.Club, 2),
                new Card(Suits.Club, 2),
                new Card(Suits.Club, 2),
                null
            };
            Assert.Throws<CardInPokerHandIsNullException>(() => _cardGameService.GetWinningHands(new List<Hand> { new Hand(cards) }));
        }

        public void ShouldThrowRepeatedCardsExceptionOnGetWinningHands()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Club, 2),
                new Card(Suits.Club, 3),
                new Card(Suits.Heart, 3),
                new Card(Suits.Heart, 2),
                new Card(Suits.Club, 2),
            };
            Assert.Throws<RepeatedCardsException>(() => _cardGameService.GetWinningHands(new List<Hand> { new Hand(cards) }));
        }

        [Test]
        public void ShouldRestartGame()
        {
            _cardGameService.RestartGame();

            do
            {
                try
                {
                    _cardGameService.GetHand();
                }

                catch (NoCardsLeftException)
                {
                    break;
                }
            } while (true);

            _cardGameService.RestartGame();
            Assert.DoesNotThrow(() => _cardGameService.GetHand());
        }

        public abstract void ShouldGetSingleWinningHand(IGetWinningHandsTester<Hand> tester);

        public abstract void ShouldGetMultipleWinningHand(IGetWinningHandsTester<List<Hand>> tester);
    }
}
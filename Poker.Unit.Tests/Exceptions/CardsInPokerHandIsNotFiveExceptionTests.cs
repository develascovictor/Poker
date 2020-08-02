using NUnit.Framework;
using Poker.Enums;
using Poker.Exceptions;
using Poker.Extensions;
using Poker.Models;
using Poker.Unit.Tests.Exceptions.Base;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Exceptions
{
    [TestFixture]
    public class CardsInPokerHandIsNotFiveExceptionTests : BaseExceptionTests
    {
        [Test]
        public void ShouldInstantiateConstructorWithParameters()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Club, 3)
            };
            var hand = new Hand(cards);

            var cardsInPokerHandIsNotFiveException = new CardsInPokerHandIsNotFiveException(hand);
            ValidateException(cardsInPokerHandIsNotFiveException, $"Poker hand does not have 5 cards. [Hand: {hand.Stringify()}]");
        }
    }
}
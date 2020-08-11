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
    public class InvalidDrawOnBustHandExceptionTests : BaseExceptionTests
    {
        [Test]
        public void ShouldInstantiateConstructorWithParameters()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Club, 10),
                new Card(Suits.Heart, 10),
                new Card(Suits.Club, 2)
            };
            var hand = new Hand(cards);

            var invalidDrawOnBustHandException = new InvalidDrawOnBustHandException(hand);
            ValidateException(invalidDrawOnBustHandException, $"Cannot add a new card on a bust hand. [Hand: {hand.Stringify()}]");
        }
    }
}
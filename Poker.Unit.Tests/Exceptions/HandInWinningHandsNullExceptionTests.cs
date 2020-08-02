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
    public class HandInWinningHandsNullExceptionTests : BaseExceptionTests
    {
        [Test]
        public void ShouldInstantiateConstructorWithParameters()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Club, 3)
            };
            var hand = new Hand(cards);
            var hands = new List<Hand>
            {
                hand,
                null
            };

            var handInWinningHandsNullException = new HandInWinningHandsNullException(hands);
            ValidateException(handInWinningHandsNullException, $"A hand in winning hands is null. [Hand: {hands.Stringify()}]");
        }
    }
}
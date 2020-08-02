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
    public class CardInPokerHandIsNullExceptionTests : BaseExceptionTests
    {
        [Test]
        public void ShouldInstantiateConstructorWithParameters()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Club, 3),
                null
            };
            var hand = new Hand(cards);

            var cardInPokerHandIsNullException = new CardInPokerHandIsNullException(hand);
            ValidateException(cardInPokerHandIsNullException, $"A Card in poker hand is null. [Hand: {hand.Stringify()}]");
        }
    }
}
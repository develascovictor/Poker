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
    public class RepeatedCardsExceptionTests : BaseExceptionTests
    {
        [Test]
        public void ShouldInstantiateConstructorWithParameters()
        {
            var repeatedCards = new List<Card>
            {
                new Card(Suits.Club, 2),
                new Card(Suits.Club, 3),
            };
            var repeatedCardsException = new RepeatedCardsException(repeatedCards);
            ValidateException(repeatedCardsException, $"Some cards are repeated. [Repeated Cards: {repeatedCards.Stringify()}]");
        }
    }
}
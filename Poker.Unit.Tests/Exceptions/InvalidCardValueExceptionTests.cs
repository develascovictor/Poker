using NUnit.Framework;
using Poker.Exceptions;
using Poker.Unit.Tests.Exceptions.Base;

namespace Poker.Unit.Tests.Exceptions
{
    [TestFixture]
    public class InvalidCardValueExceptionTests : BaseExceptionTests
    {
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void ShouldInstantiateConstructorWithParameters(int cardValue)
        {
            var invalidCardValueException = new InvalidCardValueException(cardValue);
            ValidateException(invalidCardValueException, $"Invalid card value. [Card Value: {cardValue}]");
        }
    }
}
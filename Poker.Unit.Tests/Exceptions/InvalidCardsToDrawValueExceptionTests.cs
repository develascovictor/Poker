using NUnit.Framework;
using Poker.Exceptions;
using Poker.Unit.Tests.Exceptions.Base;

namespace Poker.Unit.Tests.Exceptions
{
    [TestFixture]
    public class InvalidCardsToDrawValueExceptionTests : BaseExceptionTests
    {
        [TestCase(-2)]
        [TestCase(-1)]
        [TestCase(0)]
        public void ShouldInstantiateConstructorWithParameters(int cardsToDraw)
        {
            var invalidCardsToDrawValueException = new InvalidCardsToDrawValueException(cardsToDraw);
            ValidateException(invalidCardsToDrawValueException, $"Cards to draw value must be greater than 0. [Cards To Draw: {cardsToDraw}]");
        }
    }
}
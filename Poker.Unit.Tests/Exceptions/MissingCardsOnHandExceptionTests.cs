using NUnit.Framework;
using Poker.Exceptions;
using Poker.Unit.Tests.Exceptions.Base;

namespace Poker.Unit.Tests.Exceptions
{
    [TestFixture]
    public class MissingCardsOnHandExceptionTests : BaseExceptionTests
    {
        [Test]
        public void ShouldInstantiateEmptyConstructor()
        {
            var missingCardsOnHandException = new MissingCardsOnHandException();
            ValidateException(missingCardsOnHandException, "Cards in poker hand is null.");
        }
    }
}
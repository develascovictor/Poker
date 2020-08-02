using NUnit.Framework;
using Poker.Exceptions;
using Poker.Unit.Tests.Exceptions.Base;

namespace Poker.Unit.Tests.Exceptions
{
    [TestFixture]
    public class NoCardsLeftExceptionTests : BaseExceptionTests
    {
        [Test]
        public void ShouldInstantiateEmptyConstructor()
        {
            var noCardsLeftException = new NoCardsLeftException();
            ValidateException(noCardsLeftException, "No cards left in deck.");
        }
    }
}
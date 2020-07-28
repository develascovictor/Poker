using NUnit.Framework;
using Poker.Exceptions;
using Poker.Unit.Tests.Exceptions.Base;

namespace Poker.Unit.Tests.Exceptions
{
    [TestFixture]
    public class MissingWinningHandsExceptionTests : BaseExceptionTests
    {
        [Test]
        public void ShouldInstantiateEmptyConstructor()
        {
            var missingWinningHandsException = new MissingWinningHandsException();
            ValidateException(missingWinningHandsException, "Winning cards list is null or empty.");
        }
    }
}
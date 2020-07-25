using NUnit.Framework;
using Poker.Enums;
using Poker.Models;
using Poker.Unit.Tests.Models.Base;

namespace Poker.Unit.Tests.Models
{
    [TestFixture]
    public class CardTests : BaseModelTests<Card>
    {
        [TestCase(nameof(Card.Suit))]
        [TestCase(nameof(Card.Value))]
        public void ShouldValidatePublicAccessorProperty(string propertyName)
        {
            ValidatePublicAccessorProperty(propertyName);
        }

        [Test]
        public void ShouldInstantiateEmptyConstructor()
        {
            var card = new Card();
            Assert.AreEqual(card.Suit, default(Suits));
            Assert.AreEqual(card.Value, default(int));
        }
    }
}
using NUnit.Framework;
using Poker.Models;
using Poker.Unit.Tests.Models.Base;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Models
{
    [TestFixture]
    public class DeckTests : BaseModelTests<Deck>
    {
        [TestCase(nameof(Deck.Cards))]
        public void ShouldValidatePublicAccessorProperty(string propertyName)
        {
            ValidatePublicAccessorProperty(propertyName);
        }

        [Test]
        public void ShouldInstantiateEmptyConstructor()
        {
            var deck = new Deck();
            Assert.AreEqual(deck.Cards, new List<Card>());
        }
    }
}
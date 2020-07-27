using NUnit.Framework;
using Poker.Enums;
using Poker.Models;
using Poker.Unit.Tests.Models.Base;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Models
{
    [TestFixture]
    public class HandTests : BaseModelTests<Hand>
    {
        [TestCase(nameof(Hand.Cards))]
        public void ShouldValidatePrivateSettableProperty(string propertyName)
        {
            ValidatePrivateSettableProperty(propertyName);
        }

        [TestCase(nameof(Hand.Rank))]
        [TestCase(nameof(Hand.Details))]
        public void ShouldValidatePublicAccessorProperty(string propertyName)
        {
            ValidateNonSettableProperty(propertyName);
        }

        [Test]
        public void ShouldInstantiateEmptyConstructor()
        {
            var hand = new Hand(null);
            Assert.AreEqual(hand.Cards, new List<Card>());
            Assert.AreEqual(hand.Rank, Ranks.HighCard);
            Assert.AreEqual(hand.Details, string.Empty);
        }
    }
}
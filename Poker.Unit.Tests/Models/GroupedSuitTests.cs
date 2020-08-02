using NUnit.Framework;
using Poker.Enums;
using Poker.Models;
using Poker.Unit.Tests.Models.Base;

namespace Poker.Unit.Tests.Models
{
    [TestFixture]
    public class GroupedSuitTests : BaseModelTests<GroupedSuit>
    {
        [TestCase(nameof(GroupedSuit.Suit))]
        [TestCase(nameof(GroupedSuit.Count))]
        public void ShouldValidatePublicAccessorProperty(string propertyName)
        {
            ValidatePublicAccessorProperty(propertyName);
        }

        [Test]
        public void ShouldInstantiateEmptyConstructor()
        {
            var groupedSuit = new GroupedSuit();
            Assert.AreEqual(groupedSuit.Suit, default(Suits));
            Assert.AreEqual(groupedSuit.Count, default(int));
        }
    }
}
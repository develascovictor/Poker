using NUnit.Framework;
using Poker.Models;
using Poker.Unit.Tests.Models.Base;

namespace Poker.Unit.Tests.Models
{
    [TestFixture]
    public class GroupedValueTests : BaseModelTests<GroupedValue>
    {
        [TestCase(nameof(GroupedValue.Value))]
        [TestCase(nameof(GroupedValue.Count))]
        public void ShouldValidatePublicAccessorProperty(string propertyName)
        {
            ValidatePublicAccessorProperty(propertyName);
        }

        [Test]
        public void ShouldInstantiateEmptyConstructor()
        {
            var groupedValue = new GroupedValue();
            Assert.AreEqual(groupedValue.Value, default(int));
            Assert.AreEqual(groupedValue.Count, default(int));
        }
    }
}
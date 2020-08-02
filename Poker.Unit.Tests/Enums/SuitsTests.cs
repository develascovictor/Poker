using NUnit.Framework;
using Poker.Enums;
using Poker.Unit.Tests.Enums.Base;

namespace Poker.Unit.Tests.Enums
{
    [TestFixture]
    public class SuitsTests : BaseEnumTests<Suits>
    {
        [TestCase(Suits.Heart, "Heart", 1)]
        [TestCase(Suits.Diamond, "Diamond", 2)]
        [TestCase(Suits.Club, "Club", 3)]
        [TestCase(Suits.Spade, "Spade", 4)]
        public void ShouldValidateConstantStrings(Suits enumValue, string stringValue, int intValue)
        {
            ValidateEnumValues(enumValue, stringValue, intValue);
        }
    }
}
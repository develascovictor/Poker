using NUnit.Framework;
using Poker.Enums;
using Poker.Unit.Tests.Enums.Base;

namespace Poker.Unit.Tests.Enums
{
    [TestFixture]
    public class RanksTests : BaseEnumTests<Ranks>
    {
        [TestCase(Ranks.HighCard, "High Card", 0)]
        [TestCase(Ranks.OnePair, "1 Pair", 1)]
        [TestCase(Ranks.TwoPairs, "2 Pairs", 2)]
        [TestCase(Ranks.ThreeOfAKind, "3 Of A Kind", 3)]
        [TestCase(Ranks.Straight, "Straight", 4)]
        [TestCase(Ranks.Flush, "Flush", 5)]
        [TestCase(Ranks.FullHouse, "Full House", 6)]
        [TestCase(Ranks.FourOfAKind, "4 Of A Kind", 7)]
        [TestCase(Ranks.StraightFlush, "Straight Flush", 8)]
        public void ShouldValidateConstantStrings(Ranks enumValue, string stringValue, int intValue)
        {
            ValidateEnumValues(enumValue, stringValue, intValue);
        }
    }
}
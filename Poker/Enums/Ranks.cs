using System.ComponentModel;

namespace Poker.Enums
{
    public enum Ranks
    {
        [Description("High Card")]
        HighCard = 0,
        [Description("1 Pair")]
        OnePair = 1,
        [Description("2 Pairs")]
        TwoPairs = 2,
        [Description("3 Of A Kind")]
        ThreeOfAKind = 3,
        [Description("Straight")]
        Straight = 4,
        [Description("Flush")]
        Flush = 5,
        [Description("Full House")]
        FullHouse = 6,
        [Description("4 Of A Kind")]
        FourOfAKind = 7,
        [Description("Straight Flush")]
        StraightFlush = 8
    }
}
using Poker.Models;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Services.Testers.Collections
{
    public static class Hands
    {
        public static class SingleWin
        {
            public static Hand HighCardEight => new Hand
            (
                new List<Card>
                {
                    Cards.Heart8,
                    Cards.Diamond6,
                    Cards.Heart5,
                    Cards.Heart4,
                    Cards.Heart3
                }
            );
            public static Hand HighCardKing => new Hand
            (
                new List<Card>
                {
                    Cards.DiamondKing,
                    Cards.DiamondQueen,
                    Cards.Spade8,
                    Cards.Club4,
                    Cards.Club3
                }
            );
            public static Hand HighCardAce => new Hand
            (
                new List<Card>
                {
                    Cards.DiamondAce,
                    Cards.Diamond9,
                    Cards.Diamond8,
                    Cards.Club5,
                    Cards.Diamond4
                }
            );
            public static Hand OnePairTwo => new Hand
            (
                new List<Card>
                {
                    Cards.HeartAce,
                    Cards.Heart6,
                    Cards.Spade5,
                    Cards.Spade2,
                    Cards.Club2
                }
            );
            public static Hand OnePairQueen => new Hand
            (
                new List<Card>
                {
                    Cards.HeartQueen,
                    Cards.SpadeQueen,
                    Cards.Spade9,
                    Cards.Club8,
                    Cards.Diamond3
                }
            );
            public static Hand FourOfAKindSeven => new Hand
            (
                new List<Card>
                {
                    Cards.Spade10,
                    Cards.Club7,
                    Cards.Spade7,
                    Cards.Diamond7,
                    Cards.Heart7
                }
            );
            public static Hand StraightFlush => new Hand
            (
                new List<Card>
                {
                    Cards.ClubAce,
                    Cards.ClubKing,
                    Cards.ClubQueen,
                    Cards.ClubJack,
                    Cards.Club10
                }
            );
        }

        public static class HighCardTieBreaker
        {
            public static Hand HighCardAce1 => new Hand
            (
                new List<Card>
                {
                        Cards.ClubAce,
                        Cards.Spade10,
                        Cards.Heart8,
                        Cards.Diamond6,
                        Cards.Diamond4
                }
            );
            public static Hand HighCardAce2 => new Hand
            (
                new List<Card>
                {
                        Cards.DiamondAce,
                        Cards.Club10,
                        Cards.Spade8,
                        Cards.Heart6,
                        Cards.Heart3
                }
            );
            public static Hand HighCardAce3 => new Hand
            (
                new List<Card>
                {
                        Cards.HeartAce,
                        Cards.Diamond10,
                        Cards.Club7,
                        Cards.Spade6,
                        Cards.Spade4
                }
            );
            public static Hand HighCardAce4 => new Hand
            (
                new List<Card>
                {
                        Cards.SpadeAce,
                        Cards.Heart9,
                        Cards.Diamond8,
                        Cards.Club6,
                        Cards.Club4
                }
            );
        }

        public static class OnePairTieBreaker
        {
            public static Hand OnePairKing1 => new Hand
            (
                new List<Card>
                {
                        Cards.ClubKing,
                        Cards.SpadeKing,
                        Cards.Heart8,
                        Cards.Diamond6,
                        Cards.Diamond4
                }
            );
            public static Hand OnePairKing2 => new Hand
            (
                new List<Card>
                {
                        Cards.DiamondKing,
                        Cards.HeartKing,
                        Cards.Spade8,
                        Cards.Heart6,
                        Cards.Heart3
                }
            );
            public static Hand HighCardAce1 => new Hand
            (
                new List<Card>
                {
                        Cards.HeartAce,
                        Cards.Diamond10,
                        Cards.Club7,
                        Cards.Spade6,
                        Cards.Spade4
                }
            );
            public static Hand HighCardAce2 => new Hand
            (
                new List<Card>
                {
                        Cards.SpadeAce,
                        Cards.Heart9,
                        Cards.Diamond8,
                        Cards.Club6,
                        Cards.Club4
                }
            );
        }

        public static class TwoPairTieBreaker
        {
            public static Hand TwoPairAceAnd101 => new Hand
            (
                new List<Card>
                {
                        Cards.ClubAce,
                        Cards.SpadeAce,
                        Cards.Heart10,
                        Cards.Diamond10,
                        Cards.Diamond4
                }
            );
            public static Hand TwoPairAceAnd102 => new Hand
            (
                new List<Card>
                {
                        Cards.DiamondAce,
                        Cards.HeartAce,
                        Cards.Spade10,
                        Cards.Club10,
                        Cards.Heart3
                }
            );
            public static Hand TwoPairQueenAndJack => new Hand
            (
                new List<Card>
                {
                        Cards.ClubQueen,
                        Cards.SpadeQueen,
                        Cards.HeartJack,
                        Cards.DiamondJack,
                        Cards.Diamond7
                }
            );
            public static Hand TwoPairQueenAnd9 => new Hand
            (
                new List<Card>
                {
                        Cards.DiamondQueen,
                        Cards.HeartQueen,
                        Cards.Spade9,
                        Cards.Club9,
                        Cards.Heart7
                }
            );
        }

        public static class FlushTieBreaker
        {
            public static Hand FlushClubAce => new Hand
            (
                new List<Card>
                {
                        Cards.ClubAce,
                        Cards.Club10,
                        Cards.Club8,
                        Cards.Club6,
                        Cards.Club4
                }
            );
            public static Hand FlushDiamondAce => new Hand
            (
                new List<Card>
                {
                        Cards.DiamondAce,
                        Cards.Diamond10,
                        Cards.Diamond8,
                        Cards.Diamond6,
                        Cards.Diamond3
                }
            );
            public static Hand FlushHeartAce => new Hand
            (
                new List<Card>
                {
                        Cards.HeartAce,
                        Cards.Heart10,
                        Cards.Heart7,
                        Cards.Heart6,
                        Cards.Heart4
                }
            );
            public static Hand FlushSpadeAce => new Hand
            (
                new List<Card>
                {
                        Cards.SpadeAce,
                        Cards.Spade9,
                        Cards.Spade8,
                        Cards.Spade6,
                        Cards.Spade4
                }
            );
        }

        public static class ThreeOfAKindTieBreaker
        {
            public static Hand ThreeOfAKindKing => new Hand
            (
                new List<Card>
                {
                        Cards.ClubKing,
                        Cards.SpadeKing,
                        Cards.HeartKing,
                        Cards.Diamond8,
                        Cards.Diamond7
                }
            );
            public static Hand ThreeOfAKindQueen => new Hand
            (
                new List<Card>
                {
                        Cards.DiamondQueen,
                        Cards.HeartQueen,
                        Cards.SpadeQueen,
                        Cards.ClubJack,
                        Cards.Heart2
                }
            );
        }

        public static class FullHouseTieBreaker
        {
            public static Hand FullHouseKingAndJack => new Hand
            (
                new List<Card>
                {
                        Cards.ClubKing,
                        Cards.SpadeKing,
                        Cards.HeartKing,
                        Cards.DiamondJack,
                        Cards.SpadeJack
                }
            );
            public static Hand FullHouseQueenAnd6 => new Hand
            (
                new List<Card>
                {
                        Cards.DiamondQueen,
                        Cards.HeartQueen,
                        Cards.SpadeQueen,
                        Cards.Club6,
                        Cards.Heart6
                }
            );
        }

        public static class FourOfAKindTieBreaker
        {
            public static Hand FourOfAKindKing => new Hand
            (
                new List<Card>
                {
                        Cards.ClubKing,
                        Cards.SpadeKing,
                        Cards.HeartKing,
                        Cards.DiamondKing,
                        Cards.SpadeJack
                }
            );
            public static Hand FourOfAKindQueen => new Hand
            (
                new List<Card>
                {
                        Cards.DiamondQueen,
                        Cards.HeartQueen,
                        Cards.SpadeQueen,
                        Cards.ClubQueen,
                        Cards.Heart6
                }
            );
        }

        public static class StraightTieBreaker
        {
            public static Hand StraightJack => new Hand
            (
                new List<Card>
                {
                        Cards.ClubJack,
                        Cards.Spade10,
                        Cards.Heart9,
                        Cards.Diamond8,
                        Cards.Spade7
                }
            );
            public static Hand Straight10 => new Hand
            (
                new List<Card>
                {
                        Cards.Club10,
                        Cards.Spade9,
                        Cards.Heart8,
                        Cards.Diamond7,
                        Cards.Spade6
                }
            );
        }

        public static class StraightFlushTieBreaker
        {
            public static Hand StraightFlushClubJack => new Hand
            (
                new List<Card>
                {
                        Cards.ClubJack,
                        Cards.Club10,
                        Cards.Club9,
                        Cards.Club8,
                        Cards.Club7
                }
            );
            public static Hand StraightFlushSpade10 => new Hand
            (
                new List<Card>
                {
                        Cards.Spade10,
                        Cards.Spade9,
                        Cards.Spade8,
                        Cards.Spade7,
                        Cards.Spade6
                }
            );
        }
    }
}

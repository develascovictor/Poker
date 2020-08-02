using NUnit.Framework;
using Poker.Models;
using Poker.Services;
using Poker.Services.Interfaces;
using Poker.Unit.Tests.Services.Testers;
using Poker.Unit.Tests.Services.Testers.Collections;
using Poker.Unit.Tests.Services.Testers.Interfaces;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Services
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PokerServiceTests
    {
        private IPokerService _pokerService;

        [SetUp]
        public void SetUp()
        {
            _pokerService = new PokerService();
        }

        [TestCaseSource(nameof(GetSingleWinningHandTestCases))]
        public void ShouldGetSingleWinningHand(IGetWinningHandsTester<Hand> tester)
        {
            tester.RunGetWinningHands(_pokerService);
        }

        [TestCaseSource(nameof(GetMultipleWinningHandTestCases))]
        public void ShouldGetMultipleWinningHand(IGetWinningHandsTester<List<Hand>> tester)
        {
            tester.RunGetWinningHands(_pokerService);
        }

        private static IEnumerable<IGetWinningHandsTester<Hand>> GetSingleWinningHandTestCases()
        {
            yield return new GetSingleWinningHandTester
            {
                Description = "Straight Win - High Card",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.StraightWin.HighCardEight
                },
                ExpectedResult = Hands.SingleWin.StraightWin.HighCardEight
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Straight Win - One Pair",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.StraightWin.OnePairQueen,
                    Hands.SingleWin.StraightWin.HighCardEight,
                    Hands.SingleWin.StraightWin.HighCardAce
                },
                ExpectedResult = Hands.SingleWin.StraightWin.OnePairQueen
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Straight Win - Four Of A Kind",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.StraightWin.HighCardEight,
                    Hands.SingleWin.StraightWin.HighCardAce,
                    Hands.SingleWin.StraightWin.OnePairTwo,
                    Hands.SingleWin.StraightWin.OnePairQueen,
                    Hands.SingleWin.StraightWin.FourOfAKindSeven
                },
                ExpectedResult = Hands.SingleWin.StraightWin.FourOfAKindSeven
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - High Card",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.HighCardTieBreaker.HighCardAce1,
                    Hands.SingleWin.HighCardTieBreaker.HighCardAce2,
                    Hands.SingleWin.HighCardTieBreaker.HighCardAce3,
                    Hands.SingleWin.HighCardTieBreaker.HighCardAce4
                },
                ExpectedResult = Hands.SingleWin.HighCardTieBreaker.HighCardAce1
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - One Pair",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.OnePairTieBreaker.OnePairKing1,
                    Hands.SingleWin.OnePairTieBreaker.OnePairKing2,
                    Hands.SingleWin.OnePairTieBreaker.HighCardAce1,
                    Hands.SingleWin.OnePairTieBreaker.HighCardAce2
                },
                ExpectedResult = Hands.SingleWin.OnePairTieBreaker.OnePairKing1
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Two Pair - On High Card",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.TwoPairTieBreaker.TwoPairAceAnd101,
                    Hands.SingleWin.TwoPairTieBreaker.TwoPairAceAnd102
                },
                ExpectedResult = Hands.SingleWin.TwoPairTieBreaker.TwoPairAceAnd101
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Two Pair - On Highest Pair Value",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.TwoPairTieBreaker.TwoPairQueenAndJack,
                    Hands.SingleWin.TwoPairTieBreaker.TwoPairQueenAnd9
                },
                ExpectedResult = Hands.SingleWin.TwoPairTieBreaker.TwoPairQueenAndJack
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Flush",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.FlushTieBreaker.FlushClubAce,
                    Hands.SingleWin.FlushTieBreaker.FlushDiamondAce,
                    Hands.SingleWin.FlushTieBreaker.FlushHeartAce,
                    Hands.SingleWin.FlushTieBreaker.FlushSpadeAce
                },
                ExpectedResult = Hands.SingleWin.FlushTieBreaker.FlushClubAce
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Three Of A Kind",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.ThreeOfAKindTieBreaker.ThreeOfAKindKing,
                    Hands.SingleWin.ThreeOfAKindTieBreaker.ThreeOfAKindQueen
                },
                ExpectedResult = Hands.SingleWin.ThreeOfAKindTieBreaker.ThreeOfAKindKing
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Full House",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.FullHouseTieBreaker.FullHouseKingAndJack,
                    Hands.SingleWin.FullHouseTieBreaker.FullHouseQueenAnd6
                },
                ExpectedResult = Hands.SingleWin.FullHouseTieBreaker.FullHouseKingAndJack
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Straight",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.StraightTieBreaker.StraightJack,
                    Hands.SingleWin.StraightTieBreaker.Straight10
                },
                ExpectedResult = Hands.SingleWin.StraightTieBreaker.StraightJack
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Straight Flush",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.StraightFlushTieBreaker.StraightFlushClubJack,
                    Hands.SingleWin.StraightFlushTieBreaker.StraightFlushSpade10
                },
                ExpectedResult = Hands.SingleWin.StraightFlushTieBreaker.StraightFlushClubJack
            };
        }

        private static IEnumerable<IGetWinningHandsTester<List<Hand>>> GetMultipleWinningHandTestCases()
        {
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - High Card",
                Hands = new List<Hand>
                {
                    Hands.MultipleWin.HighCardTieBreaker.HighCardAce1,
                    Hands.MultipleWin.HighCardTieBreaker.HighCardAce2,
                    Hands.MultipleWin.HighCardTieBreaker.HighCardAce3,
                    Hands.MultipleWin.HighCardTieBreaker.HighCardAce4
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.MultipleWin.HighCardTieBreaker.HighCardAce1,
                    Hands.MultipleWin.HighCardTieBreaker.HighCardAce2
                }
            };
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - One Pair",
                Hands = new List<Hand>
                {
                    Hands.MultipleWin.OnePairTieBreaker.OnePairKing1,
                    Hands.MultipleWin.OnePairTieBreaker.OnePairKing2,
                    Hands.MultipleWin.OnePairTieBreaker.OnePairJack,
                    Hands.MultipleWin.OnePairTieBreaker.HighCardAce
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.MultipleWin.OnePairTieBreaker.OnePairKing1,
                    Hands.MultipleWin.OnePairTieBreaker.OnePairKing2,
                }
            };
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - Two Pair",
                Hands = new List<Hand>
                {
                    Hands.MultipleWin.TwoPairTieBreaker.TwoPairAceAnd101,
                    Hands.MultipleWin.TwoPairTieBreaker.TwoPairAceAnd102
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.MultipleWin.TwoPairTieBreaker.TwoPairAceAnd101,
                    Hands.MultipleWin.TwoPairTieBreaker.TwoPairAceAnd102
                }
            };
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - Flush",
                Hands = new List<Hand>
                {
                    Hands.MultipleWin.FlushTieBreaker.FlushClubAce,
                    Hands.MultipleWin.FlushTieBreaker.FlushDiamondAce,
                    Hands.MultipleWin.FlushTieBreaker.FlushHeartAce,
                    Hands.MultipleWin.FlushTieBreaker.FlushSpadeAce
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.MultipleWin.FlushTieBreaker.FlushClubAce,
                    Hands.MultipleWin.FlushTieBreaker.FlushDiamondAce,
                    Hands.MultipleWin.FlushTieBreaker.FlushHeartAce
                },
            };
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - Straight",
                Hands = new List<Hand>
                {
                    Hands.MultipleWin.StraightTieBreaker.StraightJack1,
                    Hands.MultipleWin.StraightTieBreaker.StraightJack2
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.MultipleWin.StraightTieBreaker.StraightJack1,
                    Hands.MultipleWin.StraightTieBreaker.StraightJack2
                }
            };
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - Straight Flush",
                Hands = new List<Hand>
                {
                    Hands.MultipleWin.StraightFlushTieBreaker.StraightFlushClubJack,
                    Hands.MultipleWin.StraightFlushTieBreaker.StraightFlushSpadeJack
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.MultipleWin.StraightFlushTieBreaker.StraightFlushClubJack,
                    Hands.MultipleWin.StraightFlushTieBreaker.StraightFlushSpadeJack
                }
            };
        }
    }
}
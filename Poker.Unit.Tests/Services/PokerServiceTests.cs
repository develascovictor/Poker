using NUnit.Framework;
using Poker.Enums;
using Poker.Exceptions;
using Poker.Models;
using Poker.Services;
using Poker.Unit.Tests.Services.Base;
using Poker.Unit.Tests.Services.Testers;
using Poker.Unit.Tests.Services.Testers.Collections;
using Poker.Unit.Tests.Services.Testers.Interfaces;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Services
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PokerServiceTests : BaseCardGameServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            _initialHandCardCount = 5;
            _cardGameService = new PokerService();
        }

        [TestCaseSource(nameof(GetSingleWinningHandTestCases))]
        public override void ShouldGetSingleWinningHand(IGetWinningHandsTester<Hand> tester)
        {
            tester.RunGetWinningHands(_cardGameService);
        }

        [TestCaseSource(nameof(GetMultipleWinningHandTestCases))]
        public override void ShouldGetMultipleWinningHand(IGetWinningHandsTester<List<Hand>> tester)
        {
            tester.RunGetWinningHands(_cardGameService);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(7)]
        public void ShouldThrowCardsInPokerHandIsNotFiveExceptionOnGetWinningHands(int numberOfRecords)
        {
            var cards = new List<Card>();

            for (var i = 0; i < numberOfRecords; i++)
            {
                cards.Add(new Card(Suits.Club, i + 2));
            }

            Assert.Throws<CardsInPokerHandIsNotFiveException>(() => _cardGameService.GetWinningHands(new List<Hand> { new Hand(cards) }));
        }

        [Test]
        public void ShouldThrowMissingCardsOnHandExceptionWithNullCardsOnGetWinningHands()
        {
            Assert.Throws<CardsInPokerHandIsNotFiveException>(() => _cardGameService.GetWinningHands(new List<Hand> { new Hand(null) }));
        }

        private static IEnumerable<IGetWinningHandsTester<Hand>> GetSingleWinningHandTestCases()
        {
            yield return new GetSingleWinningHandTester
            {
                Description = "Straight Win - High Card",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.StraightWin.HighCardEight
                },
                ExpectedResult = Hands.Poker.SingleWin.StraightWin.HighCardEight
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Straight Win - One Pair",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.StraightWin.OnePairQueen,
                    Hands.Poker.SingleWin.StraightWin.HighCardEight,
                    Hands.Poker.SingleWin.StraightWin.HighCardAce
                },
                ExpectedResult = Hands.Poker.SingleWin.StraightWin.OnePairQueen
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Straight Win - Four Of A Kind",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.StraightWin.HighCardEight,
                    Hands.Poker.SingleWin.StraightWin.HighCardAce,
                    Hands.Poker.SingleWin.StraightWin.OnePairTwo,
                    Hands.Poker.SingleWin.StraightWin.OnePairQueen,
                    Hands.Poker.SingleWin.StraightWin.FourOfAKindSeven
                },
                ExpectedResult = Hands.Poker.SingleWin.StraightWin.FourOfAKindSeven
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - High Card",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.HighCardTieBreaker.HighCardAce1,
                    Hands.Poker.SingleWin.HighCardTieBreaker.HighCardAce2,
                    Hands.Poker.SingleWin.HighCardTieBreaker.HighCardAce3,
                    Hands.Poker.SingleWin.HighCardTieBreaker.HighCardAce4
                },
                ExpectedResult = Hands.Poker.SingleWin.HighCardTieBreaker.HighCardAce1
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - One Pair",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.OnePairTieBreaker.OnePairKing1,
                    Hands.Poker.SingleWin.OnePairTieBreaker.OnePairKing2,
                    Hands.Poker.SingleWin.OnePairTieBreaker.HighCardAce1,
                    Hands.Poker.SingleWin.OnePairTieBreaker.HighCardAce2
                },
                ExpectedResult = Hands.Poker.SingleWin.OnePairTieBreaker.OnePairKing1
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Two Pair - On High Card",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.TwoPairTieBreaker.TwoPairAceAnd101,
                    Hands.Poker.SingleWin.TwoPairTieBreaker.TwoPairAceAnd102
                },
                ExpectedResult = Hands.Poker.SingleWin.TwoPairTieBreaker.TwoPairAceAnd101
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Two Pair - On Highest Pair Value",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.TwoPairTieBreaker.TwoPairQueenAndJack,
                    Hands.Poker.SingleWin.TwoPairTieBreaker.TwoPairQueenAnd9
                },
                ExpectedResult = Hands.Poker.SingleWin.TwoPairTieBreaker.TwoPairQueenAndJack
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Flush",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.FlushTieBreaker.FlushClubAce,
                    Hands.Poker.SingleWin.FlushTieBreaker.FlushDiamondAce,
                    Hands.Poker.SingleWin.FlushTieBreaker.FlushHeartAce,
                    Hands.Poker.SingleWin.FlushTieBreaker.FlushSpadeAce
                },
                ExpectedResult = Hands.Poker.SingleWin.FlushTieBreaker.FlushClubAce
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Three Of A Kind",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.ThreeOfAKindTieBreaker.ThreeOfAKindKing,
                    Hands.Poker.SingleWin.ThreeOfAKindTieBreaker.ThreeOfAKindQueen
                },
                ExpectedResult = Hands.Poker.SingleWin.ThreeOfAKindTieBreaker.ThreeOfAKindKing
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Full House",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.FullHouseTieBreaker.FullHouseKingAndJack,
                    Hands.Poker.SingleWin.FullHouseTieBreaker.FullHouseQueenAnd6
                },
                ExpectedResult = Hands.Poker.SingleWin.FullHouseTieBreaker.FullHouseKingAndJack
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Straight",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.StraightTieBreaker.StraightJack,
                    Hands.Poker.SingleWin.StraightTieBreaker.Straight10
                },
                ExpectedResult = Hands.Poker.SingleWin.StraightTieBreaker.StraightJack
            };
            yield return new GetSingleWinningHandTester
            {
                Description = "Tie Breaker - Straight Flush",
                Hands = new List<Hand>
                {
                    Hands.Poker.SingleWin.StraightFlushTieBreaker.StraightFlushClubJack,
                    Hands.Poker.SingleWin.StraightFlushTieBreaker.StraightFlushSpade10
                },
                ExpectedResult = Hands.Poker.SingleWin.StraightFlushTieBreaker.StraightFlushClubJack
            };
        }

        private static IEnumerable<IGetWinningHandsTester<List<Hand>>> GetMultipleWinningHandTestCases()
        {
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - High Card",
                Hands = new List<Hand>
                {
                    Hands.Poker.MultipleWin.HighCardTieBreaker.HighCardAce1,
                    Hands.Poker.MultipleWin.HighCardTieBreaker.HighCardAce2,
                    Hands.Poker.MultipleWin.HighCardTieBreaker.HighCardAce3,
                    Hands.Poker.MultipleWin.HighCardTieBreaker.HighCardAce4
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.Poker.MultipleWin.HighCardTieBreaker.HighCardAce1,
                    Hands.Poker.MultipleWin.HighCardTieBreaker.HighCardAce2
                }
            };
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - One Pair",
                Hands = new List<Hand>
                {
                    Hands.Poker.MultipleWin.OnePairTieBreaker.OnePairKing1,
                    Hands.Poker.MultipleWin.OnePairTieBreaker.OnePairKing2,
                    Hands.Poker.MultipleWin.OnePairTieBreaker.OnePairJack,
                    Hands.Poker.MultipleWin.OnePairTieBreaker.HighCardAce
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.Poker.MultipleWin.OnePairTieBreaker.OnePairKing1,
                    Hands.Poker.MultipleWin.OnePairTieBreaker.OnePairKing2,
                }
            };
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - Two Pair",
                Hands = new List<Hand>
                {
                    Hands.Poker.MultipleWin.TwoPairTieBreaker.TwoPairAceAnd101,
                    Hands.Poker.MultipleWin.TwoPairTieBreaker.TwoPairAceAnd102
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.Poker.MultipleWin.TwoPairTieBreaker.TwoPairAceAnd101,
                    Hands.Poker.MultipleWin.TwoPairTieBreaker.TwoPairAceAnd102
                }
            };
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - Flush",
                Hands = new List<Hand>
                {
                    Hands.Poker.MultipleWin.FlushTieBreaker.FlushClubAce,
                    Hands.Poker.MultipleWin.FlushTieBreaker.FlushDiamondAce,
                    Hands.Poker.MultipleWin.FlushTieBreaker.FlushHeartAce,
                    Hands.Poker.MultipleWin.FlushTieBreaker.FlushSpadeAce
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.Poker.MultipleWin.FlushTieBreaker.FlushClubAce,
                    Hands.Poker.MultipleWin.FlushTieBreaker.FlushDiamondAce,
                    Hands.Poker.MultipleWin.FlushTieBreaker.FlushHeartAce
                },
            };
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - Straight",
                Hands = new List<Hand>
                {
                    Hands.Poker.MultipleWin.StraightTieBreaker.StraightJack1,
                    Hands.Poker.MultipleWin.StraightTieBreaker.StraightJack2
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.Poker.MultipleWin.StraightTieBreaker.StraightJack1,
                    Hands.Poker.MultipleWin.StraightTieBreaker.StraightJack2
                }
            };
            yield return new GetMultipleWinningHandsTester
            {
                Description = "Tie Breaker - Straight Flush",
                Hands = new List<Hand>
                {
                    Hands.Poker.MultipleWin.StraightFlushTieBreaker.StraightFlushClubJack,
                    Hands.Poker.MultipleWin.StraightFlushTieBreaker.StraightFlushSpadeJack
                },
                ExpectedResult = new List<Hand>
                {
                    Hands.Poker.MultipleWin.StraightFlushTieBreaker.StraightFlushClubJack,
                    Hands.Poker.MultipleWin.StraightFlushTieBreaker.StraightFlushSpadeJack
                }
            };
        }
    }
}
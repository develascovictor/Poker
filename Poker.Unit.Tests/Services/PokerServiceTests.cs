using NUnit.Framework;
using Poker.Enums;
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

        [TestCaseSource(nameof(GetWinningHandsTestCases))]
        public void ShouldGetWinningHands(IGetWinningHandsTester<Hand> tester)
        {
            tester.RunGetWinningHands(_pokerService);
        }

        private static IEnumerable<IGetWinningHandsTester<Hand>> GetWinningHandsTestCases()
        {
            yield return new GetWinningHandsTester
            {
                Description = "Straight Win - High Card",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.HighCardEight
                },
                ExpectedResult = Hands.SingleWin.HighCardEight
            };
            yield return new GetWinningHandsTester
            {
                Description = "Straight Win - One Pair",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.OnePairQueen,
                    Hands.SingleWin.HighCardEight,
                    Hands.SingleWin.HighCardAce
                },
                ExpectedResult = Hands.SingleWin.OnePairQueen
            };
            yield return new GetWinningHandsTester
            {
                Description = "Straight Win - Four Of A Kind",
                Hands = new List<Hand>
                {
                    Hands.SingleWin.HighCardEight,
                    Hands.SingleWin.HighCardAce,
                    Hands.SingleWin.OnePairTwo,
                    Hands.SingleWin.OnePairQueen,
                    Hands.SingleWin.FourOfAKindSeven
                },
                ExpectedResult = Hands.SingleWin.FourOfAKindSeven
            };
            yield return new GetWinningHandsTester
            {
                Description = "Tie Breaker - High Card",
                Hands = new List<Hand>
                {
                    Hands.HighCardTieBreaker.HighCardAce1,
                    Hands.HighCardTieBreaker.HighCardAce2,
                    Hands.HighCardTieBreaker.HighCardAce3,
                    Hands.HighCardTieBreaker.HighCardAce4
                },
                ExpectedResult = Hands.HighCardTieBreaker.HighCardAce1
            };
            yield return new GetWinningHandsTester
            {
                Description = "Tie Breaker - One Pair",
                Hands = new List<Hand>
                {
                    Hands.OnePairTieBreaker.OnePairKing1,
                    Hands.OnePairTieBreaker.OnePairKing2,
                    Hands.OnePairTieBreaker.HighCardAce1,
                    Hands.OnePairTieBreaker.HighCardAce2
                },
                ExpectedResult = Hands.OnePairTieBreaker.OnePairKing1
            };
            yield return new GetWinningHandsTester
            {
                Description = "Tie Breaker - Two Pair - On High Card",
                Hands = new List<Hand>
                {
                    Hands.TwoPairTieBreaker.TwoPairAceAnd101,
                    Hands.TwoPairTieBreaker.TwoPairAceAnd102
                },
                ExpectedResult = Hands.TwoPairTieBreaker.TwoPairAceAnd101
            };
            yield return new GetWinningHandsTester
            {
                Description = "Tie Breaker - Two Pair - On Highest Pair Value",
                Hands = new List<Hand>
                {
                    Hands.TwoPairTieBreaker.TwoPairQueenAndJack,
                    Hands.TwoPairTieBreaker.TwoPairQueenAnd9
                },
                ExpectedResult = Hands.TwoPairTieBreaker.TwoPairQueenAndJack
            };
            yield return new GetWinningHandsTester
            {
                Description = "Tie Breaker - Flush",
                Hands = new List<Hand>
                {
                    Hands.FlushTieBreaker.FlushClubAce,
                    Hands.FlushTieBreaker.FlushDiamondAce,
                    Hands.FlushTieBreaker.FlushHeartAce,
                    Hands.FlushTieBreaker.FlushSpadeAce
                },
                ExpectedResult = Hands.FlushTieBreaker.FlushClubAce
            };
            yield return new GetWinningHandsTester
            {
                Description = "Tie Breaker - Three Of A Kind",
                Hands = new List<Hand>
                {
                    Hands.ThreeOfAKindTieBreaker.ThreeOfAKindKing,
                    Hands.ThreeOfAKindTieBreaker.ThreeOfAKindQueen
                },
                ExpectedResult = Hands.ThreeOfAKindTieBreaker.ThreeOfAKindKing
            };
            yield return new GetWinningHandsTester
            {
                Description = "Tie Breaker - Full House",
                Hands = new List<Hand>
                {
                    Hands.FullHouseTieBreaker.FullHouseKingAndJack,
                    Hands.FullHouseTieBreaker.FullHouseQueenAnd6
                },
                ExpectedResult = Hands.FullHouseTieBreaker.FullHouseKingAndJack
            };
            yield return new GetWinningHandsTester
            {
                Description = "Tie Breaker - Straight",
                Hands = new List<Hand>
                {
                    Hands.StraightTieBreaker.StraightJack,
                    Hands.StraightTieBreaker.Straight10
                },
                ExpectedResult = Hands.StraightTieBreaker.StraightJack
            };
            yield return new GetWinningHandsTester
            {
                Description = "Tie Breaker - Straight Flush",
                Hands = new List<Hand>
                {
                    Hands.StraightTieBreaker.StraightJack,
                    Hands.StraightTieBreaker.Straight10
                },
                ExpectedResult = Hands.StraightTieBreaker.StraightJack
            };
        }
    }
}
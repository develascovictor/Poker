using Poker.Enums;
using Poker.Exceptions;
using Poker.Models;
using Poker.Services.Base;
using Poker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Services
{
    public class PokerService : BaseCardGameService
    {
        public PokerService()
            : base(5)
        {

        }

        protected override IReadOnlyCollection<Hand> DetermineWinningHands(IReadOnlyCollection<Hand> hands)
        {
            var highestRank = hands.Max(h => h.Rank);
            var winningHands = hands.Where(r => r.Rank == highestRank).ToList();
            winningHands = ValidateTieBreaks(winningHands, highestRank);

            return winningHands;
        }

        protected override void ExtraValidations(IReadOnlyCollection<Hand> hands)
        {
            Func<Hand, bool> ifCardsIsNot5 = x => x.GetCards().Count != 5;

            if (hands.Any(ifCardsIsNot5))
            {
                throw new CardsInPokerHandIsNotFiveException(hands.First(ifCardsIsNot5));
            }
        }

        private static List<Hand> ValidateTieBreaks(List<Hand> winningHands, Ranks highestRank)
        {
            if (winningHands.Count > 1)
            {
                switch (highestRank)
                {
                    case Ranks.StraightFlush:
                    case Ranks.Straight:
                        {
                            var maxValue = winningHands.Select(SelectValuesFromStraight()).SelectMany(x => x).Max();
                            winningHands = winningHands.Where(FilterHandByCardValue(maxValue)).ToList();

                            break;
                        }

                    case Ranks.FourOfAKind:
                        {
                            var maxValue = winningHands.Select(x => x.GroupedValues.Single(y => y.Count == 4).Value).Max();
                            winningHands = winningHands.Where(x => x.GroupedValues.Single(y => y.Count == 4).Value == maxValue).ToList();

                            //Should return Max 1 record
                            break;
                        }

                    case Ranks.FullHouse:
                    case Ranks.ThreeOfAKind:
                        {
                            var maxValue = winningHands.Select(x => x.GroupedValues.Single(y => y.Count == 3).Value).Max();
                            winningHands = winningHands.Where(x => x.GroupedValues.Single(y => y.Count == 3).Value == maxValue).ToList();

                            //Should return Max 1 record
                            break;
                        }

                    case Ranks.Flush:
                    case Ranks.HighCard:
                        {
                            GetWinningHandsByFilteringOneOfEachKind(5, ref winningHands);
                            break;
                        }

                    case Ranks.TwoPairs:
                        {
                            var firstMaxPairValue = GetMaxValueFromEachPair(winningHands);
                            winningHands = winningHands.Where(FilterHandByPairCardValue(firstMaxPairValue)).ToList();

                            if (winningHands.Count > 1)
                            {
                                var secondMaxPairValue = GetMaxValueFromEachPair(winningHands, firstMaxPairValue);
                                winningHands = winningHands.Where(FilterHandByPairCardValue(secondMaxPairValue)).ToList();

                                GetWinningHandsByFilteringOneOfEachKind(1, ref winningHands);
                            }

                            //Should return Max 2 records
                            break;
                        }

                    case Ranks.OnePair:
                        {
                            var maxPairValue = GetMaxValueFromEachPair(winningHands);
                            winningHands = winningHands.Where(FilterHandByPairCardValue(maxPairValue)).ToList();

                            GetWinningHandsByFilteringOneOfEachKind(3, ref winningHands);
                            //Should return Max 4 records
                            break;
                        }
                }
            }

            return winningHands;
        }

        private static void GetWinningHandsByFilteringOneOfEachKind(int maxIterations, ref List<Hand> winningHands, int? previousMaxValue = null)
        {
            if (winningHands.Count > 1 && maxIterations > 0)
            {
                var newMaxValue = GetMaxValueFromOneOfEachKind(winningHands, previousMaxValue ?? 15);
                winningHands = winningHands.Where(FilterHandByCardValue(newMaxValue)).ToList();

                GetWinningHandsByFilteringOneOfEachKind(maxIterations - 1, ref winningHands, newMaxValue);
            }
        }

        private static int GetMaxValueFromOneOfEachKind(IReadOnlyCollection<Hand> winningHands, int? maxValue = null)
        {
            var max = winningHands.Select(SelectValuesFromOneOfEachKind()).SelectMany(x => x.Where(y => y < (maxValue ?? 15))).Max();
            return max;
        }

        private static int GetMaxValueFromEachPair(IReadOnlyCollection<Hand> winningHands, int? maxValue = null)
        {
            var max = winningHands.Select(x => x.GroupedValues.Where(y => y.Count == 2 && y.Value < (maxValue ?? 15)).Select(y => y.Value)).SelectMany(x => x).Max();
            return max;
        }

        private static Func<Hand, bool> FilterHandByCardValue(int value) => x => x.GetCards().Any(y => y.Value == value);
        private static Func<Hand, bool> FilterHandByPairCardValue(int pairValue) => x => x.GroupedValues.Where(y => y.Count == 2).Any(y => y.Value == pairValue);
        private static Func<Hand, IEnumerable<int>> SelectValuesFromOneOfEachKind() => x => x.GroupedValues.Where(y => y.Count == 1).Select(y => y.Value);
        private static Func<Hand, IEnumerable<int>> SelectValuesFromStraight() => x => x.GetCards().Select(y => y.Value == 14 ? (x.GetCards().Any(z => z.Value == 13) ? 14 : 1) : y.Value);
    }
}
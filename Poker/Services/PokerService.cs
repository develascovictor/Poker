using Poker.Enums;
using Poker.Interfaces;
using Poker.Models;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Services
{
    public class PokerService : IPokerService
    {
        /// <summary>
        /// Get winning hand from a list of hands
        /// </summary>
        /// <param name="hands"></param>
        /// <returns></returns>
        public List<Hand> GetWinningHands(List<Hand> hands)
        {
            var highestRank = hands.Max(h => h.Rank);
            var winningHands = hands.Where(r => r.Rank == highestRank).ToList();

            if (winningHands.Count != 1)
            {
                var handsForTieBreak = hands.Where(r => r.Rank == highestRank).ToList();

                switch (highestRank)
                {
                    case Ranks.StraightFlush:
                        {
                            break;
                        }

                    case Ranks.FourOfAKind:
                        {
                            break;
                        }

                    case Ranks.FullHouse:
                        {
                            break;
                        }

                    case Ranks.Flush:
                        {
                            break;
                        }

                    case Ranks.Straight:
                        {
                            break;
                        }

                    case Ranks.ThreeOfAKind:
                        {
                            break;
                        }

                    case Ranks.TwoPairs:
                        {
                            break;
                        }

                    case Ranks.OnePair:
                        {
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }

            return winningHands;
        }
    }
}
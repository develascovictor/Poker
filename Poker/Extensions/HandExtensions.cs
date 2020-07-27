using Poker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Extensions
{
    public static class HandExtensions
    {
        /// <summary>
        /// Get details from hands
        /// </summary>
        /// <param name="hands"></param>
        /// <returns></returns>
        public static string GetDetailedHand(this IEnumerable<Hand> hands)
        {
            var filteredHands = (hands ?? new List<Hand>()).Where(x => x != null).ToList();
            var message = new StringBuilder();

            for (var i = 0; i < filteredHands.Count; i++)
            {
                message.Append($"Hand {i + 1}: \n{filteredHands[i].Details}\n--------------------------\n\n");
            }

            return message.ToString();
        }

        /// <summary>
        /// Get result message for winning hands
        /// </summary>
        /// <param name="hands"></param>
        /// <returns></returns>
        public static string GetWinningHandsResultMessage(this IEnumerable<Hand> winningHands)
        {
            var filteredHands = (winningHands ?? new List<Hand>()).Where(x => x != null).ToList();

            switch (filteredHands.Count)
            {
                case 0:
                    {
                        return "\n\nThere are no winning hands.";
                    }

                case 1:
                    {
                        return $"\n\nWinning Hand is:\n\n{filteredHands.Single().Details}";
                    }

                default:
                    {
                        return $"\n\nTie between:\n\n{filteredHands.Aggregate(string.Empty, (current, hand) => current + hand.Details + "\n\n")}";
                    }
            }
        }
    }
}
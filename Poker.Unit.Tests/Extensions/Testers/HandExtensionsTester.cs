using NUnit.Framework;
using Poker.Extensions;
using Poker.Models;
using Poker.Unit.Tests.Extensions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Unit.Tests.Extensions.Testers
{
    public class HandExtensionsTester : IHandExtensionsTester
    {
        /// <summary>
        /// Test case
        /// </summary>
        public IEnumerable<Hand> Hands { get; set; }

        public void RunGetDetailedHand()
        {
            RunTest(() => GetDetailedHand(Hands), () => Hands.GetDetailedHand());
        }

        public void RunGetWinningHandsResultMessage()
        {
            RunTest(() => GetWinningHandsResultMessage(Hands), () => Hands.GetWinningHandsResultMessage());
        }

        private void RunTest(Func<string> expected, Func<string> actual)
        {
            var expectedResult = expected();
            var actualResult = actual();

            Assert.AreEqual(expectedResult, actualResult, ErrorMessage(actual));
        }

        private string GetDetailedHand(IEnumerable<Hand> hands)
        {
            var filteredHands = (hands ?? new List<Hand>()).Where(x => x != null).ToList();
            var message = new StringBuilder();

            for (var i = 0; i < filteredHands.Count; i++)
            {
                message.Append($"Hand {i + 1}: \n{filteredHands[i].Details}\n--------------------------\n\n");
            }

            return message.ToString();
        }

        private string GetWinningHandsResultMessage(IEnumerable<Hand> winningHands)
        {
            var filteredHands = (winningHands ?? new List<Hand>()).Where(x => x != null).ToList();

            switch (filteredHands.Count)
            {
                case 0:
                    {
                        return "There are no winning hands.";
                    }

                case 1:
                    {
                        return $"Winning Hand is:\n\n{filteredHands.Single().Details}";
                    }

                default:
                    {
                        return $"Tie between:\n\n{filteredHands.Aggregate(string.Empty, (current, hand) => current + hand.Details + "\n\n")}";
                    }
            }
        }

        private string ErrorMessage(Func<string> actual)
        {
            var testCase = Hands.Stringify();
            return $"{actual.Method.Name}\n{testCase}";
        }
    }
}
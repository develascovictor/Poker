using NUnit.Framework;
using Poker.Enums;
using Poker.Extensions;
using Poker.Models;
using Poker.Unit.Tests.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Unit.Tests.Models.Testers
{
    public class HandTester : IHandTester
    {
        /// <summary>
        /// Test case
        /// </summary>
        public IEnumerable<Card> Cards { get; set; }

        public void RunShouldInstantiateConstructorWithParameters()
        {
            var hand = new Hand(Cards);
            var serialized = (Cards ?? new List<Card>()).Where(x => x != null).ToList();
            var errorMessage = ErrorMessage();

            Assert.IsNotNull(hand.Cards, errorMessage);
            Assert.AreEqual(serialized.Count, hand.Cards.Count, errorMessage);

            for (var i = 0; i < serialized.Count; i++)
            {
                Assert.AreEqual(serialized[i].Suit, hand.Cards[i].Suit, GetIterationError(nameof(Card.Suit), i));
                Assert.AreEqual(serialized[i].Value, hand.Cards[i].Value, GetIterationError(nameof(Card.Value), i));
            }

            var rank = GetRank(serialized);
            Assert.AreEqual(rank, hand.Rank, errorMessage);

            var details = GetDetails(rank, serialized);
            Assert.AreEqual(details, hand.Details, errorMessage);
        }

        private static Ranks GetRank(IEnumerable<Card> cards)
        {
            if (!cards.Any())
            {
                return Ranks.HighCard;
            }

            var groupedBySuits =
                cards
                .GroupBy(c => c.Suit)
                .Select(c => new GroupedSuit { Suit = c.Key, Count = c.Count() })
                .OrderByDescending(c => c.Count)
                .ToList();

            var groupedByValues =
                cards
                .GroupBy(c => c.Value)
                .Select(c => new GroupedValue { Value = c.Key, Count = c.Count() })
                .OrderByDescending(c => c.Count)
                .ToList();

            if (groupedBySuits.Count == 1)
            {
                return IsStraight(groupedByValues) ? Ranks.StraightFlush : Ranks.Flush;
            }

            if (groupedByValues.Count == 2)
            {
                return groupedByValues[0].Count == 4 ? Ranks.FourOfAKind : Ranks.FullHouse;
            }

            if (IsStraight(groupedByValues))
            {
                return Ranks.Straight;
            }

            var maxGroupedValues = groupedByValues.Max(c => c.Count);

            switch (maxGroupedValues)
            {
                case 3:
                    {
                        return Ranks.ThreeOfAKind;
                    }

                case 2:
                    {
                        return
                            groupedByValues.Count(c => c.Count == maxGroupedValues) == 2
                            ? Ranks.TwoPairs
                            : Ranks.OnePair;
                    }
            }

            return Ranks.HighCard;
        }

        private static bool IsStraight(List<GroupedValue> cards)
        {
            //If all cards are NOT different
            if (cards.Count != 5)
            {
                return false;
            }

            var minValue = cards.Min(c => c.Value);
            var maxValue = cards.Max(c => c.Value);

            if (maxValue == 14 && minValue == 2)
            {
                minValue = 1;
                maxValue = cards.Where(c => c.Value != 14).Select(c => c.Value).Max();
            }

            return maxValue - minValue == 4;
        }

        private static string GetDetails(Ranks rank, IEnumerable<Card> cards)
        {
            return "Rank: "
                + rank.GetDescription()
                + "\nCards:"
                + cards.Aggregate(string.Empty, (current, card) => current + ("\n\t- " + card.Suit.GetDescription() + " of " + card.Description));
        }

        private string GetIterationError(string concept, int i) => $"{concept} don't match. [i: {i}]\n\n{ErrorMessage()}";

        private string ErrorMessage()
        {
            var testCase = Cards.Stringify();
            return $"{testCase}";
        }
    }
}
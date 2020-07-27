using Poker.Enums;
using Poker.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Models
{
    public class Hand
    {
        public IReadOnlyCollection<Card> Cards { get; private set; }

        public Ranks Rank => GetRank();

        public string Details =>
                    "Rank: "
                    + Rank.GetDescription()
                    + "\nCards:"
                    + Cards.Aggregate(string.Empty, (current, card) => current + ("\n\t- " + card.Suit.GetDescription() + " of " + card.Description));

        public Hand(IReadOnlyCollection<Card> cards)
        {
            Cards = cards ?? new List<Card>();
        }

        /// <summary>
        /// Evaluate rank
        /// </summary>
        /// <returns></returns>
        private Ranks GetRank()
        {
            if (!Cards.Any())
            {
                return Ranks.HighCard;
            }

            var groupedBySuits =
                Cards
                .GroupBy(c => c.Suit)
                .Select(c => new GroupedSuit { Suit = c.Key, Count = c.Count() })
                .OrderByDescending(c => c.Count)
                .ToList();

            var groupedByValues =
                Cards
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

        /// <summary>
        /// Evaluate if the set of cards are straight
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
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
    }
}
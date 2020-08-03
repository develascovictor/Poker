using NUnit.Framework;
using Poker.Enums;
using Poker.Exceptions;
using Poker.Extensions;
using Poker.Models;
using Poker.Unit.Tests.Models.Testers.Interfaces;
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
            var serializedCards = (Cards ?? new List<Card>()).Where(x => x != null).ToList();
            var errorMessage = ErrorMessage();

            var cards = hand.GetCards().ToList();
            Assert.IsNotNull(cards, errorMessage);
            Assert.AreEqual(serializedCards.Count, cards.Count, errorMessage);

            for (var i = 0; i < serializedCards.Count; i++)
            {
                Assert.AreEqual(serializedCards[i].Suit, cards[i].Suit, GetIterationError(nameof(Card.Suit), i));
                Assert.AreEqual(serializedCards[i].Value, cards[i].Value, GetIterationError(nameof(Card.Value), i));
            }

            var groupedBySuits = GetGroupedSuits(serializedCards);
            var serializedGroupedSuits = hand.GroupedSuits.ToList();
            Assert.AreEqual(groupedBySuits.Count, serializedGroupedSuits.Count, errorMessage);

            for (var i = 0; i < groupedBySuits.Count; i++)
            {
                Assert.AreEqual(groupedBySuits[i].Suit, serializedGroupedSuits[i].Suit, GetIterationError(nameof(GroupedSuit.Suit), i));
                Assert.AreEqual(groupedBySuits[i].Count, serializedGroupedSuits[i].Count, GetIterationError(nameof(GroupedSuit.Count), i));
            }

            var groupedByValues = GetGroupedValues(serializedCards);
            var serializedGroupedValues = hand.GroupedValues.ToList();
            Assert.AreEqual(groupedByValues.Count, serializedGroupedValues.Count, errorMessage);

            for (var i = 0; i < groupedByValues.Count; i++)
            {
                Assert.AreEqual(groupedByValues[i].Value, serializedGroupedValues[i].Value, GetIterationError(nameof(GroupedValue.Value), i));
                Assert.AreEqual(groupedByValues[i].Count, serializedGroupedValues[i].Count, GetIterationError(nameof(GroupedValue.Count), i));
            }

            var rank = GetRank(serializedCards, groupedBySuits, groupedByValues);
            Assert.AreEqual(rank, hand.Rank, errorMessage);

            var details = GetDetails(rank, serializedCards);
            Assert.AreEqual(details, hand.Details, errorMessage);
        }

        public void RunShouldThrowRepeatedCardsExceptionOnConstructor()
        {
            Assert.IsNotEmpty(Cards);
            Assert.IsTrue(Cards.Where(x => x != null).GroupBy(x => new { x.Suit, x.Value }).Any(x => x.Count() > 1));
            Assert.Throws<RepeatedCardsException>(() => new Hand(Cards));
        }

        private static List<GroupedSuit> GetGroupedSuits(IEnumerable<Card> cards)
        {
            var groupedBySuits = cards
                .GroupBy(c => c.Suit)
                .Select(c => new GroupedSuit { Suit = c.Key, Count = c.Count() })
                .OrderByDescending(c => c.Count)
                .ToList();
            return groupedBySuits;
        }

        private static List<GroupedValue> GetGroupedValues(IEnumerable<Card> cards)
        {
            var groupedValues =
                cards
                .GroupBy(c => c.Value)
                .Select(c => new GroupedValue { Value = c.Key, Count = c.Count() })
                .OrderByDescending(c => c.Count)
                .ToList();
            return groupedValues;
        }

        private static Ranks GetRank(IEnumerable<Card> cards, IReadOnlyCollection<GroupedSuit> groupedSuits, IReadOnlyCollection<GroupedValue> groupedValues)
        {
            if (!cards.Any())
            {
                return Ranks.HighCard;
            }

            var isStraight = IsStraight(groupedValues);

            if (groupedSuits.Count == 1)
            {
                return isStraight ? Ranks.StraightFlush : Ranks.Flush;
            }

            if (groupedValues.Count == 2)
            {
                return groupedValues.First().Count == 4 ? Ranks.FourOfAKind : Ranks.FullHouse;
            }

            if (isStraight)
            {
                return Ranks.Straight;
            }

            var maxGroupedValues = groupedValues.Max(c => c.Count);

            switch (maxGroupedValues)
            {
                case 3:
                    {
                        return Ranks.ThreeOfAKind;
                    }

                case 2:
                    {
                        return
                            groupedValues.Count(c => c.Count == maxGroupedValues) == 2
                            ? Ranks.TwoPairs
                            : Ranks.OnePair;
                    }
            }

            return Ranks.HighCard;
        }

        private static bool IsStraight(IReadOnlyCollection<GroupedValue> cards)
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
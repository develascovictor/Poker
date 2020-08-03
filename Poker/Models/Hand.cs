using Poker.Enums;
using Poker.Exceptions;
using Poker.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Models
{
    public class Hand
    {
        private readonly IReadOnlyCollection<Card> _cards;

        public int Value { get; private set; }

        public bool CanSplit { get; private set; }

        public Ranks Rank { get; private set; }

        public string Details { get; private set; }

        public IReadOnlyCollection<GroupedSuit> GroupedSuits { get; private set; }

        public IReadOnlyCollection<GroupedValue> GroupedValues { get; private set; }

        public Hand(IEnumerable<Card> cards)
        {
            _cards = (cards ?? new List<Card>()).Where(x => x != null).ToList();

            if (!_cards.GroupBy(x => new { x.Suit, x.Value }).All(x => x.Count() == 1))
            {
                throw new RepeatedCardsException(cards);
            }

            Value = GetValue();

            CanSplit = _cards.Count == 2 && _cards.GroupBy(x => x.Value).Any(x => x.Count() == 2);

            GroupedSuits =
                _cards
                .GroupBy(c => c.Suit)
                .Select(c => new GroupedSuit { Suit = c.Key, Count = c.Count() })
                .OrderByDescending(c => c.Count)
                .ToList();
            GroupedValues =
                _cards
                .GroupBy(c => c.Value)
                .Select(c => new GroupedValue { Value = c.Key, Count = c.Count() })
                .OrderByDescending(c => c.Count)
                .ToList();

            Rank = GetRank();

            Details =
                "Rank: "
                + Rank.GetDescription()
                + "\nCards:"
                + _cards.Aggregate(string.Empty, (current, card) => current + ("\n\t- " + card.Suit.GetDescription() + " of " + card.Description));
        }

        /// <summary>
        /// Obtain cards in hand
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<Card> GetCards()
        {
            return _cards;
        }

        /// <summary>
        /// Evaluate value
        /// </summary>
        /// <returns></returns>
        private int GetValue()
        {
            const int maxValue = 21;

            //Get sum from all cards except Ace
            //If Jack, Queen or King; return 10
            var value = _cards.Where(x => x.Value != 14).Sum(x => x.Value >= 10 && x.Value <= 13 ? 10 : x.Value);
            var aces = _cards.Where(x => x.Value == 14).ToList();

            //If there is at least one Ace
            if (aces.Any())
            {
                if (value + 11 > maxValue)
                {
                    //Each ace will count as 1
                    return value + aces.Count;
                }

                //Return an ace as 11 and the rest as 1
                return value + 11 + (aces.Count - 1);
            }

            return value;
        }

        /// <summary>
        /// Evaluate rank
        /// </summary>
        /// <returns></returns>
        private Ranks GetRank()
        {
            if (!_cards.Any())
            {
                return Ranks.HighCard;
            }

            var isStraight = IsStraight();

            if (GroupedSuits.Count == 1)
            {
                return isStraight ? Ranks.StraightFlush : Ranks.Flush;
            }

            if (GroupedValues.Count == 2)
            {
                return GroupedValues.First().Count == 4 ? Ranks.FourOfAKind : Ranks.FullHouse;
            }

            if (isStraight)
            {
                return Ranks.Straight;
            }

            var maxGroupedValues = GroupedValues.Max(c => c.Count);

            switch (maxGroupedValues)
            {
                case 3:
                    {
                        return Ranks.ThreeOfAKind;
                    }

                case 2:
                    {
                        return
                            GroupedValues.Count(c => c.Count == maxGroupedValues) == 2
                            ? Ranks.TwoPairs
                            : Ranks.OnePair;
                    }
            }

            return Ranks.HighCard;
        }

        /// <summary>
        /// Evaluate if the set of cards are straight
        /// </summary>
        /// <returns></returns>
        private bool IsStraight()
        {
            //If at least one card is equal to another one (if all 5 are NOT different)
            if (GroupedValues.Count != 5)
            {
                return false;
            }

            var minValue = GroupedValues.Min(c => c.Value);
            var maxValue = GroupedValues.Max(c => c.Value);

            //If you got an Ace and a 2
            if (maxValue == 14 && minValue == 2)
            {
                //Set the Ace value as 1
                minValue = 1;
                //Get max value ignoring the original Ace
                maxValue = GroupedValues.Where(c => c.Value != 14).Select(c => c.Value).Max();
            }

            return maxValue - minValue == 4;
        }
    }
}
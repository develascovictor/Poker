using Poker.Enums;
using Poker.Extensions;
using Poker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    class Program
    {
        #region Main
        static void Main()
        {
            //Used for iterations
            int i;
            //All cards that have been pulled from deck will be stored here
            var deck = new Deck();

            //Fill deck with cards
            foreach (var suit in Enum.GetValues(typeof(Suits)).Cast<Suits>())
            {
                for (i = 2; i <= 14; i++)
                {
                    deck.Cards.Add(new Card { Value = i, Suit = suit });
                }
            }

            //Playing hands
            var hands = new List<Hand>
            {
                GetHand(ref deck),
                GetHand(ref deck),
                GetHand(ref deck),
                GetHand(ref deck),
                GetHand(ref deck)
            };

            #region For Testing
            //List<Card> cards1 = new List<Card>
            //        {
            //            new Card { Suit = Suits.Diamond, Value = 2 },
            //            new Card { Suit = Suits.Spade, Value = 2 },
            //            new Card { Suit = Suits.Spade, Value = 3 },
            //            new Card { Suit = Suits.Diamond, Value = 3 },
            //            new Card { Suit = Suits.Diamond, Value = 9 },
            //        };

            //List<Card> cards2 = new List<Card>
            //        {
            //            new Card { Suit = Suits.Club, Value = 2 },
            //            new Card { Suit = Suits.Heart, Value = 6 },
            //            new Card { Suit = Suits.Club, Value = 6 },
            //            new Card { Suit = Suits.Diamond, Value = 6 },
            //            new Card { Suit = Suits.Spade, Value = 6 },
            //        };

            //foreach (Card card in cards1)
            //{
            //    deck.Cards.Remove(deck.Cards.FirstOrDefault(c => c.Suit == card.Suit && c.Value == card.Value));
            //}

            //foreach (Card card in cards2)
            //{
            //    deck.Cards.Remove(deck.Cards.FirstOrDefault(c => c.Suit == card.Suit && c.Value == card.Value));
            //}

            //List<Hand> hands = new List<Hand>
            //{
            //    new Hand
            //    {
            //        Cards = cards1,
            //        Rank = GetHandRank(cards1)
            //    },
            //    new Hand
            //    {
            //        Cards = cards2,
            //        Rank = GetHandRank(cards2)
            //    },
            //    GetHand(ref deck),
            //    GetHand(ref deck),
            //    GetHand(ref deck),
            //    GetHand(ref deck),
            //    GetHand(ref deck),
            //    GetHand(ref deck),
            //    GetHand(ref deck)
            //};
            #endregion

            i = 0;
            var message = new StringBuilder();

            foreach (var hand in hands)
            {
                i++;
                hand.Details =
                    "Hand "
                    + i
                    + ": \nRank: "
                    + hand.Rank.GetDescription()
                    + "\nCards:"
                    + hand.Cards.Aggregate(String.Empty, (current, card) => current + ("\n\t- " + card.Suit.GetDescription() + " of " + card.Value.GetCardValueDescription()));
                message.Append(hand.Details);
                message.Append("\n--------------------------\n\n");
            }

            var winningHands = GetWinningHands(hands);

            if (winningHands.Count == 1)
            {
                message.Append("\n\nWinning Hand is:\n\n" + winningHands[0].Details);
            }

            else
            {
                message.Append("\n\nTie between:\n\n" + winningHands.Aggregate(String.Empty, (current, hand) => current + hand.Details + "\n\n"));
            }

            #region Console Set
            int windowHeight = 65;
            bool success = false;

            do
            {
                try
                {
                    Console.WindowHeight = windowHeight;
                    success = true;
                }

                catch
                {
                    windowHeight -= 5;
                }
            }
            while (!success);
            #endregion

            Console.WriteLine(message);
            Console.ReadLine();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get winning hand from a list of hands
        /// </summary>
        /// <param name="hands"></param>
        /// <returns></returns>
        private static List<Hand> GetWinningHands(List<Hand> hands)
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

        /// <summary>
        /// Get a hand of 5 cards alongside it's rank
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        private static Hand GetHand(ref Deck deck)
        {
            var cards = GetCards(ref deck);
            var rank = GetRank(cards);
            var hand = new Hand { Cards = cards, Rank = rank };

            return hand;
        }

        /// <summary>
        /// Get set of 5 cards
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        private static List<Card> GetCards(ref Deck deck)
        {
            //Evaluate if there are at least 5 cards for this hand.
            if (deck.Cards.Count < 5)
            {
                throw new Exception("No cards left in deck.");
            }

            int index;
            var random = new Random();
            var hand = new List<Card>();

            for (int i = 0; i < 5; i++)
            {
                index = random.Next(0, deck.Cards.Count - 1);

                hand.Add
                (
                    new Card
                    {
                        Suit = deck.Cards[index].Suit,
                        Value = deck.Cards[index].Value
                    }
                );

                //Remove card from deck
                deck.Cards.RemoveAt(index);
            }

            //Order the cards from lowest to highest value and then order them by suits
            hand = hand.OrderBy(c => c.Value).ThenBy(c => c.Suit).ToList();

            return hand;
        }

        /// <summary>
        /// Evaluate rank
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        private static Ranks GetRank(List<Card> cards)
        {
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

        /// <summary>
        /// Evaluate if the set of cards are straight
        /// </summary>
        /// <param name="cards"></param>
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
        #endregion
    }
}
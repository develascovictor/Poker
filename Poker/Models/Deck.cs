using Poker.Enums;
using Poker.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Models
{
    public class Deck
    {
        public List<Card> Cards { get; private set; }

        public Deck()
        {
            FillCards();
        }

        /// <summary>
        /// Get a hand of 5 cards with validated rank
        /// </summary>
        /// <returns></returns>
        public Hand GetHand()
        {
            var cards = GetCards();
            var hand = new Hand(cards);

            return hand;
        }

        /// <summary>
        /// Get set of 5 cards
        /// </summary>
        /// <returns></returns>
        private List<Card> GetCards()
        {
            //Evaluate if there are at least 5 cards for this hand.
            if (Cards.Count < 5)
            {
                throw new NoCardsLeftException();
            }

            int index;
            var random = new Random();
            var hand = new List<Card>();

            for (int i = 0; i < 5; i++)
            {
                index = random.Next(0, Cards.Count - 1);
                hand.Add(new Card(Cards[index].Suit, Cards[index].Value));

                //Remove card from deck
                Cards.RemoveAt(index);
            }

            //Order the cards from lowest to highest value and then order them by suits
            hand = hand.OrderBy(c => c.Value).ThenBy(c => c.Suit).ToList();

            return hand;
        }

        /// <summary>
        /// Initial method to fill a deck of cards
        /// </summary>
        private void FillCards()
        {
            //Instantiate list
            Cards = new List<Card>();

            //Used for iterations
            int i;

            //Obtain list of all suits
            var suits = Enum.GetValues(typeof(Suits)).Cast<Suits>();

            //Fill deck with cards
            foreach (var suit in suits)
            {
                for (i = 2; i <= 14; i++)
                {
                    Cards.Add(new Card(suit, i));
                }
            }
        }
    }
}
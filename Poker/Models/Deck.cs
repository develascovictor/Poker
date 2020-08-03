using Poker.Enums;
using Poker.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Models
{
    public class Deck
    {
        private readonly Random _random;

        private IList<Card> _cards;

        public Deck()
        {
            _random = new Random();
            FillCards();
        }

        /// <summary>
        /// Get copy of remaining deck. This will not modify the cards in the actual deck.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Card> GetCopyOfRemainingCardsInDeck()
        {
            return _cards.Select(x => x).ToList();
        }

        /// <summary>
        /// Get set of N cards
        /// </summary>
        /// <param name="cardsToDraw"></param>
        /// <returns></returns>
        public List<Card> DrawCards(int cardsToDraw)
        {
            if (cardsToDraw <= 0)
            {
                throw new InvalidCardsToDrawValueException(cardsToDraw);
            }

            //Evaluate if there are at least N cards to draw
            if (_cards.Count < cardsToDraw)
            {
                throw new NoCardsLeftException();
            }

            var index = 0;
            var hand = new List<Card>();

            for (int i = 0; i < cardsToDraw; i++)
            {
                index = _random.Next(0, _cards.Count - 1);
                hand.Add(new Card(_cards[index].Suit, _cards[index].Value));

                //Remove card from deck
                _cards.RemoveAt(index);
            }

            //Order the cards from lowest to highest value and then order them by suits
            hand = hand.OrderBy(c => c.Value).ThenBy(c => c.Suit).ToList();

            return hand;
        }

        /// <summary>
        /// Initial method to fill a deck of cards
        /// </summary>
        public void FillCards()
        {
            //Instantiate list
            _cards = new List<Card>();

            //Used for iterations
            int i;

            //Obtain list of all suits
            var suits = Enum.GetValues(typeof(Suits)).Cast<Suits>();

            //Fill deck with cards
            foreach (var suit in suits)
            {
                for (i = 2; i <= 14; i++)
                {
                    _cards.Add(new Card(suit, i));
                }
            }
        }
    }
}
using Poker.Enums;
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
                    Cards.Add(new Card { Value = i, Suit = suit });
                }
            }
        }
    }
}
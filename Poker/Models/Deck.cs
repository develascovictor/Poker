using System.Collections.Generic;

namespace Poker.Models
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            Cards = new List<Card>();
        }
    }
}
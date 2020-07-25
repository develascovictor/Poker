using Poker.Enums;
using System.Collections.Generic;

namespace Poker.Models
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public Ranks Rank { get; set; }
        public string Details { get; set; }

        public Hand()
        {
            Cards = new List<Card>();
            Rank = Ranks.HighCard;
            Details = string.Empty;
        }
    }
}
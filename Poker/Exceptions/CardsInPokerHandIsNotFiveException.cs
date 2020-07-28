using Poker.Extensions;
using Poker.Models;
using System;

namespace Poker.Exceptions
{
    public sealed class CardsInPokerHandIsNotFiveException : Exception
    {
        public CardsInPokerHandIsNotFiveException(Hand hand)
            : base($"Poker hand does not have 5 cards. [Hand: {hand.Stringify()}]")
        {
        }
    }
}
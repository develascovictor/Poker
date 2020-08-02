using Poker.Extensions;
using Poker.Models;
using System;

namespace Poker.Exceptions
{
    public sealed class CardInPokerHandIsNullException : Exception
    {
        public CardInPokerHandIsNullException(Hand hand)
            : base($"A Card in poker hand is null. [Hand: {hand.Stringify()}]")
        {
        }
    }
}
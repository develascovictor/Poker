using Poker.Extensions;
using Poker.Models;
using System;

namespace Poker.Exceptions
{
    public sealed class InvalidDrawOnBustHandException : Exception
    {
        public InvalidDrawOnBustHandException(Hand hand)
            : base($"Cannot add a new card on a bust hand. [Hand: {hand.Stringify()}]")
        {
        }
    }
}
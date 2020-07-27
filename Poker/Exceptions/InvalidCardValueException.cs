using System;

namespace Poker.Exceptions
{
    public sealed class InvalidCardValueException : Exception
    {
        public InvalidCardValueException(int cardValue)
            : base($"Invalid card value. [Card Value: {cardValue}]")
        {
        }
    }
}
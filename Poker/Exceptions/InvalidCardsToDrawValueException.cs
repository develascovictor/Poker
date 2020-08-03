using System;

namespace Poker.Exceptions
{
    public sealed class InvalidCardsToDrawValueException : Exception
    {
        public InvalidCardsToDrawValueException(int cardsToDraw)
            : base($"Cards to draw value must be greater than 0. [Cards To Draw: {cardsToDraw}]")
        {
        }
    }
}
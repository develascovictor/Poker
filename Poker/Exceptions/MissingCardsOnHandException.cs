using System;

namespace Poker.Exceptions
{
    public sealed class MissingCardsOnHandException : Exception
    {
        public MissingCardsOnHandException()
            : base("Cards in poker hand is null.")
        {
        }
    }
}
using System;

namespace Poker.Exceptions
{
    public sealed class NoCardsLeftException : Exception
    {
        public NoCardsLeftException()
            : base("No cards left in deck.")
        {
        }
    }
}
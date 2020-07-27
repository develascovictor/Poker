using System;

namespace Poker.Exceptions
{
    public sealed class MissingWinningHandsException : Exception
    {
        public MissingWinningHandsException()
            : base($"Winning cards list is null or empty.")
        {
        }
    }
}
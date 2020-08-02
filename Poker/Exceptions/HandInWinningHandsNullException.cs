using Poker.Extensions;
using Poker.Models;
using System;
using System.Collections.Generic;

namespace Poker.Exceptions
{
    public sealed class HandInWinningHandsNullException : Exception
    {
        public HandInWinningHandsNullException(IEnumerable<Hand> hands)
            : base($"A hand in winning hands is null. [Hand: {hands.Stringify()}]")
        {
        }
    }
}
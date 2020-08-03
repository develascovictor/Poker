using Poker.Extensions;
using Poker.Models;
using System;
using System.Collections.Generic;

namespace Poker.Exceptions
{
    public sealed class RepeatedCardsException : Exception
    {
        public RepeatedCardsException(IEnumerable<Card> repeatedCards)
            : base($"Some cards are repeated. [Repeated Cards: {repeatedCards.Stringify()}]")
        {
        }
    }
}
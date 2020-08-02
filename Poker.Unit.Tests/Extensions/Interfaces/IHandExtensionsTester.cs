using Poker.Models;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Extensions.Interfaces
{
    public interface IHandExtensionsTester
    {
        IEnumerable<Hand> Hands { get; set; }
        void RunGetDetailedHand();
        void RunGetWinningHandsResultMessage();
    }
}
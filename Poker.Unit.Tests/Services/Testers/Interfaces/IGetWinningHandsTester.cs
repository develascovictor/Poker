using Poker.Models;
using Poker.Services.Interfaces;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Services.Testers.Interfaces
{
    public interface IGetWinningHandsTester<T>
    {
        string Description { get; set; }
        List<Hand> Hands { get; set; }
        T ExpectedResult { get; set; }
        void RunGetWinningHands(ICardGameService pokerService);
    }
}
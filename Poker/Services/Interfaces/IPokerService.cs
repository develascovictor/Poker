using Poker.Models;
using System.Collections.Generic;

namespace Poker.Services.Interfaces
{
    public interface IPokerService
    {
        /// <summary>
        /// Get winning hand from a list of hands
        /// </summary>
        /// <param name="hands"></param>
        /// <returns></returns>
        IReadOnlyCollection<Hand> GetWinningHands(IReadOnlyCollection<Hand> hands);
    }
}
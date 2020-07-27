using Poker.Models;
using System.Collections.Generic;

namespace Poker.Interfaces
{
    public interface IPokerService
    {
        /// <summary>
        /// Get winning hand from a list of hands
        /// </summary>
        /// <param name="hands"></param>
        /// <returns></returns>
        List<Hand> GetWinningHands(List<Hand> hands);
    }
}

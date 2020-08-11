using Poker.Models;
using System.Collections.Generic;

namespace Poker.Services.Interfaces
{
    public interface ICardGameService
    {
        /// <summary>
        /// Get hand of cards
        /// </summary>
        /// <returns></returns>
        Hand GetHand();

        /// <summary>
        /// Get winning hand from a list of hands
        /// </summary>
        /// <param name="hands"></param>
        /// <returns></returns>
        IReadOnlyCollection<Hand> GetWinningHands(IReadOnlyCollection<Hand> hands);

        /// <summary>
        /// Reset service so a game can start from the beggining
        /// </summary>
        void RestartGame();
    }
}
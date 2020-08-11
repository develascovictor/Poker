using Poker.Models;

namespace Poker.Services.Interfaces
{
    public interface IBlackJackService : ICardGameService
    {
        /// <summary>
        /// Add an extra card to hand
        /// </summary>
        /// <returns></returns>
        Hand AskForCard(Hand hand);
    }
}
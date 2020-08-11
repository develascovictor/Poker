using Poker.Exceptions;
using Poker.Models;
using Poker.Services.Base;
using Poker.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Services
{
    public class BlackJackService : BaseCardGameService, IBlackJackService
    {
        public BlackJackService()
            : base(2)
        {

        }

        protected override IReadOnlyCollection<Hand> DetermineWinningHands(IReadOnlyCollection<Hand> hands)
        {
            var maxValue = hands.Select(x => x.Value).Where(x => x <= 21).DefaultIfEmpty(0).Max();
            var winningHands = hands.Where(x => x.Value == maxValue).ToList();

            return winningHands;
        }

        protected override void ExtraValidations(IReadOnlyCollection<Hand> hands)
        {
            //No need to validate anything else
        }

        /// <summary>
        /// Add an extra card to hand
        /// </summary>
        /// <returns></returns>
        public Hand AskForCard(Hand hand)
        {
            //Cannot ask for card if hand is a bust
            if (hand.Value > 21)
            {
                throw new InvalidDrawOnBustHandException(hand);
            }

            var card = _deck.DrawCards(1).Single();
            var cards = hand.GetCards().ToList();
            cards.Add(card);

            var updatedHand = new Hand(cards);
            return updatedHand;
        }
    }
}
using Poker.Exceptions;
using Poker.Models;
using Poker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Services.Base
{
    public abstract class BaseCardGameService : ICardGameService
    {
        protected readonly Deck _deck;
        protected readonly int _initialHandCardCount;

        public BaseCardGameService(int initialHandCardCount)
        {
            _deck = new Deck();
            _initialHandCardCount = initialHandCardCount;
        }

        /// <summary>
        /// Get a hand of N cards with validated rank
        /// </summary>
        /// <returns></returns>
        public Hand GetHand()
        {
            var cards = _deck.DrawCards(_initialHandCardCount);
            var hand = new Hand(cards);

            return hand;
        }

        /// <summary>
        /// Get winning hand from a list of hands
        /// </summary>
        /// <param name="hands"></param>
        /// <returns></returns>
        public IReadOnlyCollection<Hand> GetWinningHands(IReadOnlyCollection<Hand> hands)
        {
            ValidateWinningHands(hands);
            var winningHands = DetermineWinningHands(hands);

            return winningHands;
        }

        /// <summary>
        /// Reset service so a game can start from the beggining
        /// </summary>
        public void RestartGame()
        {
            _deck.FillCards();
        }

        private void ValidateWinningHands(IReadOnlyCollection<Hand> hands)
        {
            if (hands?.Any() != true)
            {
                throw new MissingWinningHandsException();
            }

            if (hands.Any(x => x == null))
            {
                throw new HandInWinningHandsNullException(hands);
            }

            if (hands.Any(x => x.GetCards() == null))
            {
                throw new MissingCardsOnHandException();
            }

            Func<Hand, bool> ifAnyCardIsNull = x => x.GetCards().Any(y => y == null);

            if (hands.Any(ifAnyCardIsNull))
            {
                throw new CardInPokerHandIsNullException(hands.First(ifAnyCardIsNull));
            }

            var repeatedGroups = hands.SelectMany(x => x.GetCards()).GroupBy(x => new { x.Suit, x.Value }).Where(x => x.Count() > 1).ToList();

            if (repeatedGroups.Any())
            {
                var repeatedCards = repeatedGroups.Select(x => new Card(x.Key.Suit, x.Key.Value)).ToList();
                throw new RepeatedCardsException(repeatedCards);
            }

            ExtraValidations(hands);
        }

        protected abstract IReadOnlyCollection<Hand> DetermineWinningHands(IReadOnlyCollection<Hand> hands);
        protected abstract void ExtraValidations(IReadOnlyCollection<Hand> hands);
    }
}

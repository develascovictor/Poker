using NUnit.Framework;
using Poker.Enums;
using Poker.Exceptions;
using Poker.Models;
using Poker.Unit.Tests.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Unit.Tests.Models
{
    [TestFixture]
    public class DeckTests : BaseModelTests<Deck>
    {
        const int HandSize = 5;

        [TestCase(nameof(Deck.Cards))]
        public void ShouldValidatePrivateSettableProperty(string propertyName)
        {
            ValidatePrivateSettableProperty(propertyName);
        }

        [Test]
        public void ShouldInstantiateEmptyConstructor()
        {
            var deck = new Deck();
            var cards = GetDeckOfCards();

            Assert.IsNotEmpty(deck.Cards);
            Assert.AreEqual(cards.Count, deck.Cards.Count);

            for (var i = 0; i < cards.Count; i++)
            {
                Assert.AreEqual(cards[i].Suit, deck.Cards[i].Suit, GetIterationError(nameof(Card.Suit), i));
                Assert.AreEqual(cards[i].Value, deck.Cards[i].Value, GetIterationError(nameof(Card.Value), i));
            }
        }

        [Test]
        public void ShouldGetHand()
        {
            var deck = new Deck();
            var deckSize = deck.Cards.Count();

            var firstHand = deck.GetHand();
            ValidateHandAndDeck(firstHand, deck, deckSize, 1);

            var secondHand = deck.GetHand();
            ValidateHandAndDeck(secondHand, deck, deckSize, 2);

            foreach (Card card in firstHand.Cards)
            {
                Assert.IsFalse(secondHand.Cards.Any(x => x.Suit == card.Suit && x.Value == card.Value));
            }
        }

        [Test]
        public void ShouldThrowNoCardsLeftExceptionOnGetHand()
        {
            var deck = new Deck();
            var deckSize = deck.Cards.Count() - 2;
            var neededIterations = deckSize / HandSize;

            for (var i = 0; i < neededIterations; i++)
            {
                deck.GetHand();
            }

            Assert.Throws<NoCardsLeftException>(() => deck.GetHand());
        }

        private void ValidateHandAndDeck(Hand hand, Deck deck, int initialDeckSize, int iterations)
        {
            Assert.IsNotNull(hand);
            Assert.IsNotEmpty(hand.Cards);
            Assert.AreEqual(HandSize, hand.Cards.Count());
            Assert.AreEqual(initialDeckSize - (HandSize * iterations), deck.Cards.Count());

            foreach (Card card in hand.Cards)
            {
                Assert.IsFalse(deck.Cards.Any(x => x.Suit == card.Suit && x.Value == card.Value));
            }
        }

        private string GetIterationError(string concept, int i) => $"{concept} don't match. [i: {i}]";

        private List<Card> GetDeckOfCards()
        {
            //Instantiate list
            var cards = new List<Card>();

            //Used for iterations
            int i;

            //Obtain list of all suits
            var suits = Enum.GetValues(typeof(Suits)).Cast<Suits>();

            //Fill deck with cards
            foreach (var suit in suits)
            {
                for (i = 2; i <= 14; i++)
                {
                    cards.Add(new Card(suit, i));
                }
            }

            return cards;
        }
    }
}
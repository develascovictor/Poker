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
        [Test]
        public void ShouldInstantiateEmptyConstructor()
        {
            var deck = new Deck();
        }

        [Test]
        public void ShouldGetCopyOfRemainingDeck()
        {
            var deck = new Deck();
            var actualDeckOfCards = deck.GetCopyOfRemainingCardsInDeck().ToList();
            var expectedDeckOfCards = GetDeckOfCards();

            Assert.IsNotEmpty(actualDeckOfCards);
            Assert.AreEqual(expectedDeckOfCards.Count, actualDeckOfCards.Count);

            for (var i = 0; i < expectedDeckOfCards.Count; i++)
            {
                Assert.AreEqual(expectedDeckOfCards[i].Suit, actualDeckOfCards[i].Suit, GetIterationError(nameof(Card.Suit), i));
                Assert.AreEqual(expectedDeckOfCards[i].Value, actualDeckOfCards[i].Value, GetIterationError(nameof(Card.Value), i));
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void ShouldDrawCards(int handSize)
        {
            var deck = new Deck();
            var deckSize = deck.GetCopyOfRemainingCardsInDeck().Count();

            var firstSetOfCards = deck.DrawCards(handSize);
            ValidateHandAndDeck(firstSetOfCards, deck, handSize, deckSize, 1);

            var secondSetOfCards = deck.DrawCards(handSize);
            ValidateHandAndDeck(secondSetOfCards, deck, handSize, deckSize, 2);

            foreach (Card card in firstSetOfCards)
            {
                Assert.IsFalse(secondSetOfCards.Any(x => x.Suit == card.Suit && x.Value == card.Value));
            }
        }

        [Test]
        public void ShouldThrowNoCardsLeftExceptionOnGetHand()
        {
            var deck = new Deck();
            var deckSize = deck.GetCopyOfRemainingCardsInDeck().Count();
            deck.DrawCards(deckSize);

            Assert.Throws<NoCardsLeftException>(() => deck.DrawCards(1));
        }

        [Test]
        public void ShouldFillCards()
        {
            var deck = new Deck();
            var deckSize = deck.GetCopyOfRemainingCardsInDeck().Count();
            deck.DrawCards(deckSize);
            deck.FillCards();

            ShouldGetCopyOfRemainingDeck();
        }

        private void ValidateHandAndDeck(IReadOnlyCollection<Card> cards, Deck deck, int handSize, int initialDeckSize, int iterations)
        {
            var remainingCards = deck.GetCopyOfRemainingCardsInDeck();
            Assert.IsNotEmpty(cards);
            Assert.AreEqual(handSize, cards.Count());
            Assert.AreEqual(initialDeckSize - (handSize * iterations), remainingCards.Count());
            Assert.IsTrue(cards.GroupBy(x => new { x.Suit, x.Value }).All(x => x.Count() == 1));

            foreach (Card card in cards)
            {
                Assert.IsFalse(remainingCards.Any(x => x.Suit == card.Suit && x.Value == card.Value));
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
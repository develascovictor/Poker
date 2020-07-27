using NUnit.Framework;
using Poker.Enums;
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
                Assert.AreEqual(cards[i].Suit, deck.Cards[i].Suit, GetIterationError("Suits", i));
                Assert.AreEqual(cards[i].Value, deck.Cards[i].Value, GetIterationError("Values", i));
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
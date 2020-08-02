using NUnit.Framework;
using Poker.Enums;
using Poker.Exceptions;
using Poker.Models;
using Poker.Unit.Tests.Models.Base;

namespace Poker.Unit.Tests.Models
{
    [TestFixture]
    public class CardTests : BaseModelTests<Card>
    {
        [TestCase(nameof(Card.Suit))]
        [TestCase(nameof(Card.Value))]
        public void ShouldValidatePrivateSettablePropertyy(string propertyName)
        {
            ValidatePrivateSettableProperty(propertyName);
        }

        [TestCase(nameof(Card.Description))]
        public void ShouldValidateNonSettableProperty(string propertyName)
        {
            ValidateNonSettableProperty(propertyName);
        }

        [TestCase(Suits.Club, 2)]
        [TestCase(Suits.Club, 3)]
        [TestCase(Suits.Club, 4)]
        [TestCase(Suits.Diamond, 5)]
        [TestCase(Suits.Diamond, 6)]
        [TestCase(Suits.Diamond, 7)]
        [TestCase(Suits.Heart, 8)]
        [TestCase(Suits.Heart, 9)]
        [TestCase(Suits.Heart, 10)]
        [TestCase(Suits.Spade, 11)]
        [TestCase(Suits.Spade, 12)]
        [TestCase(Suits.Spade, 13)]
        [TestCase(Suits.Spade, 14)]
        public void ShouldInstantiateConstructorWithParameters(Suits suit, int value)
        {
            var card = new Card(suit, value);
            Assert.AreEqual(suit, card.Suit);
            Assert.AreEqual(value, card.Value);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        public void ShouldGetValueDescription(int value)
        {
            var card = new Card(Suits.Club, value);
            var cardDescription = card.GetValueDescription();

            switch (value)
            {
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    {
                        Assert.AreEqual(value.ToString(), cardDescription);
                        break;
                    }

                case 11:
                    {
                        Assert.AreEqual("Jack", cardDescription);
                        break;
                    }

                case 12:
                    {
                        Assert.AreEqual("Queen", cardDescription);
                        break;
                    }

                case 13:
                    {
                        Assert.AreEqual("King", cardDescription);
                        break;
                    }

                case 14:
                    {
                        Assert.AreEqual("Ace", cardDescription);
                        break;
                    }

                default:
                    {
                        throw new InvalidCardValueException(value);
                    }
            }
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(16)]
        public void ShouldThrowInvalidCardValueExceptionOnGetValueDescription(int value)
        {
            Assert.Throws<InvalidCardValueException>(() => new Card(Suits.Club, value));
        }
    }
}
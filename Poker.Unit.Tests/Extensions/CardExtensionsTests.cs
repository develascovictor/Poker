using NUnit.Framework;
using Poker.Exceptions;
using Poker.Extensions;
using System;

namespace Poker.Unit.Tests.Extensions
{
    [TestFixture]
    public class CardExtensionsTests
    {
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
        public void ShouldGetCardValueDescription(int value)
        {
            var cardDescription = value.GetCardValueDescription();

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
                        throw new ArgumentException($"Invalid test case. [Value {value}]");
                    }
            }
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(16)]
        public void ShouldThrowInvalidCardValueExceptionOnGetCardValueDescription(int value)
        {
            Assert.Throws<InvalidCardValueException>(() => value.GetCardValueDescription());
        }
    }
}
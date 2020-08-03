using NUnit.Framework;
using Poker.Enums;
using Poker.Models;
using Poker.Unit.Tests.Models.Base;
using Poker.Unit.Tests.Models.Testers;
using Poker.Unit.Tests.Models.Testers.Interfaces;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Models
{
    [TestFixture]
    public class HandTests : BaseModelTests<Hand>
    {
        [TestCase(nameof(Hand.Cards))]
        [TestCase(nameof(Hand.Rank))]
        [TestCase(nameof(Hand.Details))]
        [TestCase(nameof(Hand.GroupedSuits))]
        [TestCase(nameof(Hand.GroupedValues))]
        public void ShouldValidatePrivateSettableProperty(string propertyName)
        {
            ValidatePrivateSettableProperty(propertyName);
        }

        [TestCaseSource(nameof(TestCases))]
        public void ShouldInstantiateConstructorWithParameters(IHandTester tester)
        {
            tester.RunShouldInstantiateConstructorWithParameters();
        }

        [TestCaseSource(nameof(InvalidTestCases))]
        public void ShouldThrowRepeatedCardsExceptionOnConstructor(IHandTester tester)
        {
            tester.RunShouldThrowRepeatedCardsExceptionOnConstructor();
        }

        private static IEnumerable<IHandTester> TestCases()
        {
            yield return new HandTester
            {
                Cards = new List<Card>
                {
                    new Card(Suits.Club, 2),
                    new Card(Suits.Club, 7),
                    new Card(Suits.Spade, 3),
                    new Card(Suits.Club, 12),
                    new Card(Suits.Heart, 13),
                    new Card(Suits.Spade, 14)
                }
            };
            yield return new HandTester
            {
                Cards = new List<Card>
                {
                    new Card(Suits.Diamond, 5),
                    new Card(Suits.Diamond, 8),
                    new Card(Suits.Spade, 8),
                    new Card(Suits.Heart, 8),
                    new Card(Suits.Heart, 9),
                    new Card(Suits.Diamond, 11)
                }
            };
            yield return new HandTester
            {
                Cards = new List<Card>
                {
                    new Card(Suits.Diamond, 5),
                    new Card(Suits.Heart, 5),
                    new Card(Suits.Diamond, 10)
                }
            };
            yield return new HandTester
            {
                Cards = new List<Card>
                {
                    null,
                    new Card(Suits.Spade, 2)
                }
            };
            yield return new HandTester
            {
                Cards = new List<Card>
                {
                    null,
                    null
                }
            };
            yield return new HandTester
            {
                Cards = new List<Card>()
            };
            yield return new HandTester
            {
                Cards = null
            };
        }

        private static IEnumerable<IHandTester> InvalidTestCases()
        {
            yield return new HandTester
            {
                Cards = new List<Card>
                {
                    new Card(Suits.Club, 2),
                    new Card(Suits.Spade, 3),
                    new Card(Suits.Club, 12),
                    new Card(Suits.Club, 2),
                    new Card(Suits.Heart, 13)
                }
            };
            yield return new HandTester
            {
                Cards = new List<Card>
                {
                    new Card(Suits.Diamond, 5),
                    new Card(Suits.Diamond, 5),
                    new Card(Suits.Diamond, 5),
                    new Card(Suits.Diamond, 5),
                    new Card(Suits.Diamond, 5)
                }
            };
            yield return new HandTester
            {
                Cards = new List<Card>
                {
                    new Card(Suits.Diamond, 5),
                    null,
                    new Card(Suits.Diamond, 5)
                }
            };
        }
    }
}
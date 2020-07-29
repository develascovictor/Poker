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
        public void ShouldValidatePrivateSettableProperty(string propertyName)
        {
            ValidatePrivateSettableProperty(propertyName);
        }

        [TestCase(nameof(Hand.Rank))]
        [TestCase(nameof(Hand.Details))]
        public void ShouldValidatePublicAccessorProperty(string propertyName)
        {
            ValidateNonSettableProperty(propertyName);
        }

        [TestCaseSource(nameof(TestCases))]
        public void ShouldInstantiateConstructorWithParameters(IHandTester tester)
        {
            tester.RunShouldInstantiateConstructorWithParameters();
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
    }
}
using NUnit.Framework;
using Poker.Enums;
using Poker.Models;
using Poker.Unit.Tests.Extensions.Interfaces;
using Poker.Unit.Tests.Extensions.Testers;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Extensionsc
{
    [TestFixture]
    public class HandExtensionsTests
    {
        [TestCaseSource(nameof(TestCases))]
        public void ShouldGetDetailedHand(IHandExtensionsTester tester)
        {
            tester.RunGetDetailedHand();
        }

        [TestCaseSource(nameof(TestCases))]
        public void ShouldGetWinningHandsResultMessage(IHandExtensionsTester tester)
        {
            tester.RunGetWinningHandsResultMessage();
        }

        private static IEnumerable<IHandExtensionsTester> TestCases()
        {
            yield return new HandExtensionsTester
            {
                Hands = new List<Hand>
                {
                    new Hand
                    (
                        new List<Card>
                        {
                            new Card(Suits.Club, 2),
                            new Card(Suits.Club, 7),
                            new Card(Suits.Spade, 3),
                            new Card(Suits.Club, 12),
                            new Card(Suits.Heart, 13),
                            new Card(Suits.Spade, 14)
                        }
                    )
                }
            };
            yield return new HandExtensionsTester
            {
                Hands = new List<Hand>
                {
                    new Hand
                    (
                        new List<Card>
                        {
                            new Card(Suits.Diamond, 5),
                            new Card(Suits.Diamond, 8),
                            new Card(Suits.Spade, 8),
                            new Card(Suits.Heart, 8),
                            new Card(Suits.Heart, 9),
                            new Card(Suits.Diamond, 11)
                        }
                    ),
                    new Hand
                    (
                        new List<Card>
                        {
                            new Card(Suits.Spade, 2)
                        }
                    ),
                    new Hand
                    (
                        new List<Card>
                        {
                            new Card(Suits.Club, 3),
                            new Card(Suits.Spade, 3)
                        }
                    )
                }
            };
            yield return new HandExtensionsTester
            {
                Hands = new List<Hand>
                {
                    new Hand(new List<Card>())
                }
            };
            yield return new HandExtensionsTester
            {
                Hands = new List<Hand>
                {
                    new Hand(null)
                }
            };
            yield return new HandExtensionsTester
            {
                Hands = new List<Hand>
                {
                    null,
                    new Hand(null),
                    new Hand
                    (
                        new List<Card>
                        {
                            new Card(Suits.Diamond, 5),
                            new Card(Suits.Heart, 5),
                            new Card(Suits.Diamond, 10)
                        }
                    )
                }
            };
        }
    }
}
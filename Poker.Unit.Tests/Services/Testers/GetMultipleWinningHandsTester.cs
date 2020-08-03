﻿using NUnit.Framework;
using Poker.Models;
using Poker.Unit.Tests.Services.Testers.Base;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Unit.Tests.Services.Testers
{
    public class GetMultipleWinningHandsTester : BaseGetWinningHandTester<List<Hand>>
    {
        protected override void ValidateGetWinningHands(List<Hand> winningHands)
        {
            Assert.IsNotEmpty(winningHands);
            Assert.AreEqual(ExpectedResult.Count, winningHands.Count);

            for (var i = 0; i < ExpectedResult.Count; i++)
            {
                var winningHand = winningHands[i];
                Assert.IsNotNull(winningHand, $"Hand is null. [i: {i}]");

                var cards = winningHand.GetCards().ToList();
                Assert.IsNotEmpty(cards, $"Hand's cards is null. [i: {i}]");
                Assert.AreEqual(5, cards.Count, $"Hand's cards count doesn't match. [i: {i}]");

                for (var j = 0; j < 5; j++)
                {
                    Assert.IsNotNull(cards[j], $"Card is null. [i: {i}] [j: {j}]");

                    var expectedCards = ExpectedResult[i].GetCards().ToList();
                    Assert.AreEqual(expectedCards[j].Suit, cards[j].Suit, GetIterationError(nameof(Card.Suit), i, j));
                    Assert.AreEqual(expectedCards[j].Value, cards[j].Value, GetIterationError(nameof(Card.Value), i, j));
                }
            }
        }

        protected override void ValidateExpectedResult()
        {
            Assert.IsNotEmpty(ExpectedResult);

            for (var i = 0; i < ExpectedResult.Count; i++)
            {
                var expectedResult = ExpectedResult[i];
                Assert.IsNotNull(expectedResult, $"Expected Result is null. [i: {i}]");

                var expectedCards = expectedResult.GetCards();
                Assert.IsNotEmpty(expectedCards, $"Expected Result's cards is null. [i: {i}]");
                Assert.AreEqual(5, expectedCards.Count, $"Expected Result's cards count doesn't match. [i: {i}]");
                Assert.IsTrue(expectedCards.All(x => x != null), $"Expected Result's cards has a null record. [i: {i}]");
            }
        }

        private string GetIterationError(string concept, int i, int j) => $"\"{concept}\" doesn't match. [i: {i}] [j: {j}]";
    }
}
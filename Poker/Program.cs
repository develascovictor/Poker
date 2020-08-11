using Poker.Extensions;
using Poker.Models;
using Poker.Services;
using Poker.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Poker
{
    class Program
    {
        #region Main
        static void Main()
        {
            //Instantiate Poker service
            var pokerService = (ICardGameService)new PokerService();

            //Playing hands
            var hands = new List<Hand>
            {
                pokerService.GetHand(),
                pokerService.GetHand(),
                pokerService.GetHand(),
                pokerService.GetHand(),
                pokerService.GetHand(),
                pokerService.GetHand()
            };

            #region Console Set
            int windowHeight = 65;
            bool success = false;

            do
            {
                try
                {
                    Console.WindowHeight = windowHeight;
                    success = true;
                }

                catch
                {
                    windowHeight -= 5;
                }
            }
            while (!success);
            #endregion

            Console.WriteLine(hands.GetDetailedHand());

            var winningHands = pokerService.GetWinningHands(hands);

            Console.WriteLine(winningHands.GetWinningHandsResultMessage());
            Console.ReadLine();
        }
        #endregion
    }
}
using Poker.Exceptions;

namespace Poker.Extensions
{
    public static class CardExtensions
    {
        /// <summary>
        /// Method to obtain the Description value of a card value
        /// </summary>
        /// <param name="cardValue"></param>
        /// <returns></returns>
        public static string GetCardValueDescription(this int cardValue)
        {
            switch (cardValue)
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
                        return cardValue.ToString();
                    }

                case 11:
                    {
                        return "Jack";
                    }

                case 12:
                    {
                        return "Queen";
                    }

                case 13:
                    {
                        return "King";
                    }

                case 14:
                    {
                        return "Ace";
                    }

                default:
                    {
                        throw new InvalidCardValueException(cardValue);
                    }
            }
        }
    }
}
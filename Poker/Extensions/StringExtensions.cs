using System;
using System.ComponentModel;

namespace Poker.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Method to obtain the Description value of an enum, if it lacks a description, simply converts the enum to string
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            string enumDescription;
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                enumDescription = attributes.Length > 0 ? attributes[0].Description : enumValue.ToString();
            }

            else
            {
                enumDescription = String.Empty;
            }

            return enumDescription;
        }

        /// <summary>
        /// Method to obtain the Description value of a card value
        /// </summary>
        /// <param name="cardValue"></param>
        /// <returns></returns>
        public static string GetDescription(this int cardValue)
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
                        return "Undefined";
                    }
            }
        }
    }
}
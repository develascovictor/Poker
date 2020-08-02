using Poker.Enums;
using Poker.Exceptions;

namespace Poker.Models
{
    public class Card
    {
        public Suits Suit { get; private set; }

        public int Value { get; private set; }

        public string Description => GetValueDescription();

        public Card(Suits suit, int value)
        {
            Suit = suit;
            Value = value;

            if (value < 2 || value > 14)
            {
                throw new InvalidCardValueException(value);
            }
        }

        /// <summary>
        /// Method to obtain the Description value of a card value
        /// </summary>
        /// <returns></returns>
        public string GetValueDescription()
        {
            switch (Value)
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
                        return Value.ToString();
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
                        throw new InvalidCardValueException(Value);
                    }
            }
        }
    }
}
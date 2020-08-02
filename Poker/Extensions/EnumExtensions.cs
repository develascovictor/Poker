using System;
using System.ComponentModel;

namespace Poker.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Method to obtain the Description value of an enum, if it lacks a description, simply converts the enum to string
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            var enumDescription = string.Empty;
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                enumDescription = attributes.Length > 0 ? attributes[0].Description : enumValue.ToString();
            }

            return enumDescription;
        }
    }
}
using Newtonsoft.Json;
using System;

namespace Poker.Extensions
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Method to convert an object to a stringified JSON
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Stringify(this object value)
        {
            var json = string.Empty;

            try
            {
                json = value == null ? "null" : JsonConvert.SerializeObject(value, Formatting.Indented);
            }

            catch (Exception)
            {
                // ignored
            }

            return json;
        }
    }
}
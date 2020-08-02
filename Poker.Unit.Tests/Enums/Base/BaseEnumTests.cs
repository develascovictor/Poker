using NUnit.Framework;
using Poker.Extensions;
using System;

namespace Poker.Unit.Tests.Enums.Base
{
    public abstract class BaseEnumTests<T> where T : Enum
    {
        public void ValidateEnumValues(T enumValue, string stringValue, int intValue)
        {
            Assert.AreEqual(enumValue.GetDescription(), stringValue);
            Assert.AreEqual(Convert.ToInt32(enumValue), intValue);
        }
    }
}

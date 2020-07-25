using NUnit.Framework;
using Poker.Extensions;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace Poker.Unit.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        public enum EnumExamples
        {
            [Description("Some Description")]
            EnumWithDescription = 0,
            EnumWithoutDescription = 1
        }

        [TestCase(EnumExamples.EnumWithDescription)]
        [TestCase(EnumExamples.EnumWithoutDescription)]
        public void ShouldGetDescription(EnumExamples enumValue)
        {
            var description = enumValue.GetDescription();
            var enumToString = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumToString);

            if (fieldInfo != null)
            {
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    Assert.AreEqual(attributes[0].Description, description);
                }

                else
                {
                    Assert.AreEqual(enumToString, description);
                }
            }
        }
    }
}
using NUnit.Framework;
using Poker.Extensions;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Extensions
{
    [TestFixture]
    public class JsonExtensionsTests
    {
        [Test]
        public void ShouldReturnNullValue()
        {
            const string nullValue = "null";
            Assert.AreEqual(((object)null).Stringify(), nullValue);
            Assert.AreEqual(((string)null).Stringify(), nullValue);
            Assert.AreEqual(((int?)null).Stringify(), nullValue);
            Assert.AreEqual(((bool?)null).Stringify(), nullValue);
        }

        [Test]
        public void ShouldReturnStringifiedValue()
        {
            var emptyObject = new { };
            Assert.AreEqual(emptyObject.Stringify(), "{}");

            var person = new { Name = "Foo", LastName = "Bar" };
            var stringifiedPerson = person.Stringify();
            Assert.IsTrue(stringifiedPerson.Contains("\"Name\": \"Foo\"") && stringifiedPerson.Contains("\"LastName\": \"Bar\""));

            var list = new List<object>();

            for (var i = 0; i < 100000; i++)
            {
                list.Add(person);
            }

            var stringifiedList = list.Stringify();
            Assert.IsNotNull(stringifiedList);
            Assert.IsTrue(stringifiedList.Length > list.Count * stringifiedPerson.Length);
        }
    }
}
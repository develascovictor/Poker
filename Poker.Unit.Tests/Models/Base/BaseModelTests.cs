using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace Poker.Unit.Tests.Models.Base
{
    public abstract class BaseModelTests<T> where T : class
    {
        private const string PublicAccess = "public";
        private const string PrivateAccess = "private";
        private const string GetAccessor = "get";
        private const string SetAccessor = "set";

        protected void ValidatePublicAccessorProperty(string propertyName)
        {
            var property = GetProperty(propertyName);
            Assert.IsTrue(property.GetMethod.IsPublic, GetErrorMessage(propertyName, PublicAccess, GetAccessor, true));
            Assert.IsFalse(property.GetMethod.IsPrivate, GetErrorMessage(propertyName, PrivateAccess, GetAccessor, false));
            Assert.IsTrue(property.SetMethod.IsPublic, GetErrorMessage(propertyName, PublicAccess, SetAccessor, true));
            Assert.IsFalse(property.SetMethod.IsPrivate, GetErrorMessage(propertyName, PrivateAccess, SetAccessor, false));
        }

        protected void ValidateProtectedSettableProperty(string propertyName)
        {
            var property = GetProperty(propertyName);
            Assert.IsTrue(property.GetMethod.IsPublic, GetErrorMessage(propertyName, PublicAccess, GetAccessor, true));
            Assert.IsFalse(property.GetMethod.IsPrivate, GetErrorMessage(propertyName, PrivateAccess, GetAccessor, false));
            Assert.IsFalse(property.SetMethod.IsPublic, GetErrorMessage(propertyName, PublicAccess, SetAccessor, false));
            Assert.IsFalse(property.SetMethod.IsPrivate, GetErrorMessage(propertyName, PrivateAccess, SetAccessor, false));
        }

        protected void ValidatePrivateSettableProperty(string propertyName)
        {
            var property = GetProperty(propertyName);
            Assert.IsTrue(property.GetMethod.IsPublic, GetErrorMessage(propertyName, PublicAccess, GetAccessor, true));
            Assert.IsFalse(property.GetMethod.IsPrivate, GetErrorMessage(propertyName, PrivateAccess, GetAccessor, false));
            Assert.IsFalse(property.SetMethod.IsPublic, GetErrorMessage(propertyName, PublicAccess, SetAccessor, false));
            Assert.IsTrue(property.SetMethod.IsPrivate, GetErrorMessage(propertyName, PrivateAccess, SetAccessor, true));
        }

        protected void ValidateNonSettableProperty(string propertyName)
        {
            var property = GetProperty(propertyName);
            Assert.IsTrue(property.GetMethod.IsPublic, GetErrorMessage(propertyName, PublicAccess, SetAccessor, false));
            Assert.IsFalse(property.GetMethod.IsPrivate, GetErrorMessage(propertyName, PrivateAccess, SetAccessor, false));
            Assert.IsNull(property.SetMethod, GetNullErrorMessage(propertyName, SetAccessor));
        }

        private static PropertyInfo GetProperty(string propertyName)
        {
            var properties = typeof(T).GetProperties();
            var property = properties.FirstOrDefault(x => x.Name == propertyName);
            Assert.IsNotNull(property, $"Property '{propertyName}' not found on class {typeof(T)}.");

            return property;
        }

        private static string GetErrorMessage(string property, string access, string accessor, bool expectedValue) => $"Property '{property}' {access} {accessor} method is not {expectedValue}.";

        private static string GetNullErrorMessage(string property, string accessor) => $"Property '{property}' {accessor} method is not null.";
    }
}
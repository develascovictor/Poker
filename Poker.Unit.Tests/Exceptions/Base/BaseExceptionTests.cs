using NUnit.Framework;
using System;

namespace Poker.Unit.Tests.Exceptions.Base
{
    public abstract class BaseExceptionTests
    {
        protected void ValidateException(Exception exception, string message)
        {
            Assert.IsEmpty(exception.Data);
            Assert.IsNull(exception.Source);
            Assert.IsNull(exception.StackTrace);
            Assert.IsNull(exception.InnerException);
            Assert.AreEqual(exception.Message, message);
        }
    }
}
using NUnit.Framework;
using Poker.Models;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Models.Interfaces
{
    public interface IHandTester
    {
        IEnumerable<Card> Cards { get; set; }
        void RunShouldInstantiateConstructorWithParameters();
    }
}
using Poker.Models;
using System.Collections.Generic;

namespace Poker.Unit.Tests.Models.Testers.Interfaces
{
    public interface IHandTester
    {
        IEnumerable<Card> Cards { get; set; }
        void RunShouldInstantiateConstructorWithParameters();
    }
}
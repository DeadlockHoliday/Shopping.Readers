using Shopping.Readers.Common.Contracts;

namespace Shopping.Readers.MT.Tests.Helpers.AssertHelpers;

internal static partial class AssertBy
{
    internal static class Supply
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2045:Use Assert.Multiple", Justification = "Delegated to the top level")]
        internal static void Equal(ISupply actual, ISupply expected)
        {
            Assert.That(actual.Date, Is.EqualTo(expected.Date), "Date diff.");
        }

        internal static void Equal(ISupply[] actualOrders, ISupply[] expectedOrders)
        {
            Assert.That(actualOrders, Has.Length.EqualTo(expectedOrders.Length), "Length diff.");
            for (int i = 0; i < actualOrders.Length; i++)
            {
                Equal(actualOrders[i], expectedOrders[i]);
            }
        }
    }
}

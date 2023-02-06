using Shopping.Readers.Common.Contracts;

namespace Shopping.Readers.MT.Tests.Helpers.AssertHelpers;

internal static partial class AssertBy
{
    internal static class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2045:Use Assert.Multiple", Justification = "Delegated to the top level")]
        internal static void Equal(IProduct actualProduct, IProduct expectedProduct)
        {
            Assert.That(actualProduct.CategoryName, Is.EqualTo(expectedProduct.CategoryName));
            Assert.That(actualProduct.Info, Is.EqualTo(expectedProduct.Info));
            Assert.That(actualProduct.Price, Is.EqualTo(expectedProduct.Price));
            Assert.That(actualProduct.CurrencyCode, Is.EqualTo(expectedProduct.CurrencyCode));
        }
    }
}

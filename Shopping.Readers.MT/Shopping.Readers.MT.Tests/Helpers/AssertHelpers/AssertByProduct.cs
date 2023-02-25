using Shopping.Readers.Common.Data.Products;

namespace Shopping.Readers.MT.Tests.Helpers.AssertHelpers;

internal static partial class AssertByProduct
{
    internal static void Equal(IProduct actualProduct, IProduct expectedProduct)
    {
        Assert.That(actualProduct, Is.EqualTo(expectedProduct));
    }
}
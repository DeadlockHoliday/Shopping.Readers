

using Shopping.Readers.Common.Data;
using Shopping.Readers.Common.Data.Products;

namespace Shopping.Readers.MT.Tests.Helpers.AssertHelpers;

internal static partial class AssertBySupplyPackagePosition
{
    internal static void Equal(IEnumerable<UnprocessedSupplyPackagePosition> actualPositions, IEnumerable<UnprocessedSupplyPackagePosition> expectedPositions)
    {
        var actual = actualPositions.ToArray();
        var expected = expectedPositions.ToArray();

        Assert.That(actual, Has.Length.EqualTo(expected.Length));

        for (int i = 0; i < actual.Length; i++)
        {
            Equal(actual[i], expected[i]);
            AssertByProduct.Equal(actual[i].Product, expected[i].Product);
        }
    }

    internal static void Equal(UnprocessedSupplyPackagePosition actualPosition, UnprocessedSupplyPackagePosition expectedPosition)
    {
        Assert.That(actualPosition, Is.EqualTo(expectedPosition));
    }
}



using Shopping.Readers.Common.Supplies;

namespace Shopping.Readers.MT.Tests.Helpers.AssertHelpers;

internal static partial class AssertBy
{
    internal static class SupplyPackagePosition
    {
        internal static void Equal(IEnumerable<ISupplyPackagePosition> actualPositions, IEnumerable<ISupplyPackagePosition> expectedPositions)
        {
            var actual = actualPositions.ToArray();
            var expected = expectedPositions.ToArray();

            Assert.That(actual, Has.Length.EqualTo(expected.Length));

            for (int i = 0; i < actual.Length; i++)
            {
                Equal(actual[i], expected[i]);
                AssertBy.Product.Equal(actual[i].Product, expected[i].Product);
            }
        }

        internal static void Equal(ISupplyPackagePosition actualPosition, ISupplyPackagePosition expectedPosition)
        {
            Assert.That(actualPosition.Quantity, Is.EqualTo(expectedPosition.Quantity), "Quantity");
            Assert.That(actualPosition.TotalPrice, Is.EqualTo(expectedPosition.TotalPrice), "TotalPrice");
        }
    }
}

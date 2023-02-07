using Shopping.Readers.Common.Contracts;

namespace Shopping.Readers.MT.Tests.Helpers.AssertHelpers;

internal static partial class AssertBy
{
    internal static class OrderPosition
    {
        internal static void Equal(IEnumerable<IOrderPosition> actualPositions, IEnumerable<IOrderPosition> expectedPositions)
        {
            var actual = actualPositions.ToArray();
            var expected = expectedPositions.ToArray();

            Assert.That(actual, Has.Length.EqualTo(expected.Length));

            for (int i = 0; i < actual.Length; i++)
            {
                AssertBy.OrderPosition.Equal(actual[i], expected[i]);
            }
        }

        internal static void Equal(IOrderPosition actualPosition, IOrderPosition expectedPosition)
        {
            Product.Equal(actualPosition, expectedPosition);
            Assert.That(actualPosition.Quantity, Is.EqualTo(expectedPosition.Quantity), "Quantity");
        }
    }
}

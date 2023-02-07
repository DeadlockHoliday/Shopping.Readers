using Shopping.Readers.Common.Contracts;
using Shopping.Readers.MT.Data;
using System.Collections.Immutable;

namespace Shopping.Readers.MT.Tests.Helpers;

internal static class OrderFactory
{
    public static IOrder CreateOrder(int id, DateOnly date, params OrderPosition[] positions)
        => new Order() { Id = id, Date = date, Positions = positions.Cast<IOrderPosition>().ToImmutableArray() };
}

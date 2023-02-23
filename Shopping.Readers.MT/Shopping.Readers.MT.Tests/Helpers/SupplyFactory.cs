 
using Shopping.Readers.MT.Data;
using System.Collections.Immutable;

namespace Shopping.Readers.MT.Tests.Helpers;

internal static class SupplyFactory
{
    public static ISupply Create(DateOnly date, params ISupplyPosition[] positions)
        => new Supply() { Date = date, Positions = positions.Cast<ISupplyPosition>().ToImmutableArray() };
}

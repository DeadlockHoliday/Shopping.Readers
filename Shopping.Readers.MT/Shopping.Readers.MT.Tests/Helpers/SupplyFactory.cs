
using Shopping.Readers.Common.Supplies;
using Shopping.Readers.MT.Data;
using System.Collections.Immutable;

namespace Shopping.Readers.MT.Tests.Helpers;

internal static class SupplyFactory
{
    public static ISupplyPackage Create(DateOnly date, params ISupplyPackagePosition[] positions)
        => new Supply() { Date = date, Positions = positions.Cast<ISupplyPackagePosition>().ToImmutableArray() };
}

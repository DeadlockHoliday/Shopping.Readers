using NMoneys;
using Shopping.Readers.Common.Supplies;

namespace Shopping.Readers.MT.Data;

internal readonly struct SupplyPosition : ISupplyPackagePosition
{
    public long Quantity { get; init; }

    public long WeightGramms { get; init; }

    public Money Price { get; init; }

    public string Url { get; init; }

    public IProduct Product { get; init; }
}
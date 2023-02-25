using NMoneys;
using Shopping.Readers.Common.Products;

namespace Shopping.Readers.Common.Supplies;

public readonly struct SupplyPackagePosition : ISupplyPackagePosition
{
    public long Quantity { get; init; }

    public Money Price { get; init; }

    public IProduct Product { get; init; }
}
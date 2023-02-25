using NMoneys;
using Shopping.Readers.Common.Data.Products;
using Shopping.Readers.Common.Static;

namespace Shopping.Readers.Common.Data;

public readonly record struct SupplyPackagePosition<TProduct> : ISupplyPackagePosition
    where TProduct : IProduct
{
    public DateOnly Date { get; init; }

    public Vendor Vendor { get; init; }

    public long Quantity { get; init; }

    public Money Price { get; init; }

    public Money TotalPrice => Price.Times(Quantity);

    public TProduct Product { get; init; }
}
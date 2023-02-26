using Shopping.Readers.Common.Data.Products;

namespace Shopping.Readers.Common.Data.Supply;

public readonly record struct SupplyPosition
{
    public Invoice Invoice { get; init; }
    public Product Product { get; init; }
}

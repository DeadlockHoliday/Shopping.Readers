using Shopping.Readers.Common.Data.Products;

namespace Shopping.Readers.Common.Data;

public interface ISupplyPackagePosition<TProduct> : ISupplyPackagePosition
    where TProduct : IProduct
{
    public TProduct Product { get; init; }
}

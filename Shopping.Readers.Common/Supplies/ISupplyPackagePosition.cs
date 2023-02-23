using NMoneys;
using Shopping.Readers.Common.Products;

namespace Shopping.Readers.Common.Supplies;

/// <summary>
/// A contract for order position of a product.
/// </summary>
public interface ISupplyPackagePosition
{
    /// <summary>
    /// Number of copies of product.
    /// </summary>
    long Quantity { get; }

    /// <summary>
    /// A price for a supply.
    /// </summary>
    Money Price { get; }

    /// <summary>
    /// Total price.
    /// </summary>
    Money TotalPrice => Price.Times(Quantity);

    IProduct Product { get; }
}
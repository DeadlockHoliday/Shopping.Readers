using NMoneys;

namespace Shopping.Readers.Common.Contracts;

/// <summary>
/// A contract for an abstract product.
/// </summary>
/// <remarks>
/// A pretty dynamic stuff.
/// </remarks>
public interface IProduct
{
    /// <summary>
    /// Full info of product, whatever a vendor supplies.
    /// </summary>
    string Info { get; } // Info could diff from vendor to vendor.

    /// <summary>
    /// A category of a product.
    /// </summary>
    string CategoryName { get; }
}

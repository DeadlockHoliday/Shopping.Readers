namespace Shopping.Readers.Common.Contracts;

/// <summary>
/// A contract for an abstract product.
/// </summary>
public interface IProduct
{
    /// <summary>
    /// Full info of product, whatever a vendor supplies.
    /// Could contain:
    /// - name;
    /// - productVendor;
    /// - quantity;
    /// - size;
    /// - variation;
    /// - any other info.
    /// </summary>
    string Info { get; }

    /// <summary>
    /// A category of a product.
    /// </summary>
    string CategoryName { get; }

    /// <summary>
    /// A price for a product.
    /// </summary>
    /// <remarks>
    /// Should be (better be) in .00 validation.
    /// </remarks>
    decimal Price { get; }

    /// <summary>
    /// A currency code.
    /// </summary>
    CurrencyCode CurrencyCode { get; }
}

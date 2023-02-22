namespace Shopping.Readers.Common.Contracts.Products;

/// <summary>
/// A contract for an abstract product.
/// </summary>
public interface IProduct
{
    /// <summary>
    /// A category of a product.
    /// </summary>
    string CategoryName { get; }
}

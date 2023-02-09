namespace Shopping.Readers.Common.Contracts;

/// <summary>
/// A contract for order position of a product.
/// </summary>
public interface IOrderPosition : IProduct
{
    /// <summary>
    /// Total price.
    /// </summary>
    /// <remarks>
    /// Price x Quantity.
    /// </remarks>
    public decimal TotalPrice { get; }
}
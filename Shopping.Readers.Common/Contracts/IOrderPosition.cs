namespace Shopping.Readers.Common.Contracts;

/// <summary>
/// A contract for order position of a product.
/// </summary>
public interface IOrderPosition : IProduct
{
    /// <summary>
    /// Product quantity.
    /// </summary>
    /// <remarks>
    /// - pieces,
    /// - gramms,
    /// - liters.
    /// </remarks>
    public decimal Quantity { get; }

    /// <summary>
    /// Total price.
    /// </summary>
    /// <remarks>
    /// Price x Quantity.
    /// </remarks>
    public decimal TotalPrice { get; }
}
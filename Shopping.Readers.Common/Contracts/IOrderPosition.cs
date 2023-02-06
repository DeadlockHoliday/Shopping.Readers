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
}
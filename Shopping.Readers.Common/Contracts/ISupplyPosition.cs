using NMoneys;

namespace Shopping.Readers.Common.Contracts;

/// <summary>
/// A contract for order position of a product.
/// </summary>
public interface ISupplyPosition
{
    /// <summary>
    /// Number of copies of product.
    /// </summary>
    long Quantity { get; } // = 1

    /// <summary>
    /// Mass of a supply.
    /// </summary>
    long WeightGramms { get; } // = 1

    /// <summary>
    /// A price for a supply.
    /// </summary>
    Money Price { get; }

    /// <summary>
    /// Total price.
    /// </summary>
    Money TotalPrice => Price.Times(Quantity * WeightGramms);

    /// <summary>
    /// Url to a supply.
    /// </summary>
    string Url { get; }

    IProduct Product { get; }
}
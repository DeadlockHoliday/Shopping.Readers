namespace Shopping.Readers.Common.Contracts.Products;

public record struct UnprocessedProduct : IProduct
{
    public string CategoryName { get; init; }

    /// <summary>
    /// Full info of product, whatever a vendor supplies.
    /// </summary>
    string Info { get; init; }
}

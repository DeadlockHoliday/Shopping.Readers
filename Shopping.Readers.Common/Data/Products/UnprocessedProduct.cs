namespace Shopping.Readers.Common.Data.Products;

public readonly record struct UnprocessedProduct : IProduct
{
    public required string CategoryName { get; init; }

    public required string Info { get; init; }
}
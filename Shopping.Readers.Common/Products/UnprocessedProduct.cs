namespace Shopping.Readers.Common.Products;

public record struct UnprocessedProduct : IProduct
{
    public required string CategoryName { get; init; }

    public required string Name { get; init; }
}
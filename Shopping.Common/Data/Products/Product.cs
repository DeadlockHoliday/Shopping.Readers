using System.Diagnostics.CodeAnalysis;

namespace Shopping.Common.Data.Products;

public record class Product
{
    public bool IsProcessed => ProcessorVersion is not null;
    public virtual string ProcessorVersion => string.Empty;
    public required virtual string Info { get; init; }
    public required virtual string Category { get; init; }

    [SetsRequiredMembers]
    public Product(Product original)
    {
        Info = original.Info;
        Category = original.Category;
    }

    [SetsRequiredMembers]
    public Product(string info, string category)
    {
        this.Info = info;
        this.Category = category;
    }
}
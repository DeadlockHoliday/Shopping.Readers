using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Shopping.Readers.Common.Data.Products;

public readonly record struct ProcessedProduct : IProduct
{
    public required string CategoryName { get; init; }
    public required ReadOnlyDictionary<string, string> Details { get; init; }

    [SetsRequiredMembers]
    public ProcessedProduct(string categoryName, IDictionary<string, string> details)
    {
        CategoryName = categoryName;
        Details = new ReadOnlyDictionary<string, string>(details);
    }
}
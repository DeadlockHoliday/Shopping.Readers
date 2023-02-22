using System.Collections.ObjectModel;
using System.Text.Json;

namespace Shopping.Readers.Common.Contracts.Products;

public readonly record struct ProcessedProduct : IProduct
{
    public string CategoryName { get; init; }
    public ReadOnlyDictionary<string, object> Details { get; init; }

    public ProcessedProduct(string categoryName, IDictionary<string, object> details)
    {
        CategoryName = categoryName;
        Details = new ReadOnlyDictionary<string, object>(details);
    }

    public long? TryGetMassGramms()
    {
        if (Details.TryGetValue("Mass", out var value))
        {
            return (long)value;
        }
        return null;
    }

    public long? TryGetPieces()
    {
        if (Details.TryGetValue("Pieces", out var value))
        {
            return (long)value;
        }
        return null;
    }
}
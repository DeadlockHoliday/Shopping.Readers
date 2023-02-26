using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Shopping.Readers.Common.Data.Products;

public sealed record class ProcessedProduct : Product
{
    [SetsRequiredMembers]
    public ProcessedProduct(Product original, IDictionary<string, string> details) : base(original)
    {
        Details = new ReadOnlyDictionary<string, string>(details);
    }

    // JSON?
    private ReadOnlyDictionary<string, string> Details { get; init; }
    
    public string? GetValue(string key)
        => Details.TryGetValue(key, out var value) ? value : null;
}
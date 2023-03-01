using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Shopping.Common.Data.Products;

public sealed record class ProcessedProduct : Product
{
    [SetsRequiredMembers]
    public ProcessedProduct(
        Product original, 
        [StringSyntax(StringSyntaxAttribute.Json)] string json) : base(original)
    {
        Details = JsonDocument.Parse(json).RootElement;
    }

    public JsonElement Details { get; init; }
}
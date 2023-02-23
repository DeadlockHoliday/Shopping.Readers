using Shopping.Readers.Common.Units;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Shopping.Readers.Common.Products;

public readonly record struct ProcessedProduct : IProduct
{
    public required string Name { get; init; }
    public required string CategoryName { get; init; }
    public ReadOnlyDictionary<string, string> Details { get; init; }

    [SetsRequiredMembers]
    public ProcessedProduct(string name, string categoryName, IDictionary<string, string> details)
    {
        Name = name;
        CategoryName = categoryName;
        Details = new ReadOnlyDictionary<string, string>(details);
    }

    public Unit GetTotalAmount()
    {
        var pieces = TryGetDecimal("Pieces");
        var mass = TryGetDecimal("Mass");

        if (mass is null)
        {
            return Unit.CreatePieces(pieces ?? 0);
        }

        return Unit.CreateGrams(mass * pieces ?? 1);
    }

    private decimal? TryGetDecimal(string key)
        => Details.TryGetValue(key, out var value) ? 
            decimal.Parse(value.ToString())
            : null;
}
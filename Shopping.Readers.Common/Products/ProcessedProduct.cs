using Shopping.Readers.Common.Units;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Shopping.Readers.Common.Products;

public readonly record struct ProcessedProduct : IProduct
{
    public string CategoryName { get; init; }
    public ReadOnlyDictionary<string, string> Details { get; init; }

    public ProcessedProduct(string categoryName, IDictionary<string, string> details)
    {
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
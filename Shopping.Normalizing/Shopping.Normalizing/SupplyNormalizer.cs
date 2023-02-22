using Shopping.Normalizing.Processing.Names;
using Shopping.Normalizing.Processing.Units;
using Shopping.Readers.Common.Contracts.Products;
using System.Collections.ObjectModel;

namespace Shopping.Normalizing;

public static class SupplyNormalizer
{
    public static ProcessedProduct NormalizeLine(string line)
        => new(
            NameExtractor.Extract(line),
            new Dictionary<string, object>()
            {
                { "Mass", GetUnit(line, "г") },
                { "Pieces", GetUnit(line, "шт") },
            });

    private static long GetUnit(string line, string measure)
    {
        var units = UnitExtractor.Extract(line)
            .Select(UnitNormalizer.Normalize)
            .ToArray();
        return UnitExtractor.GetUnit(units, measure);
    }
}
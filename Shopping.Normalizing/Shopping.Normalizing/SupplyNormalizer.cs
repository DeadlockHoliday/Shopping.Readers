using Shopping.Normalizing.Processing.Names;
using Shopping.Normalizing.Processing.Units;
using Shopping.Readers.Common.Products;
using System.Collections.ObjectModel;

namespace Shopping.Normalizing;

public static class SupplyNormalizer
{
    public static ProcessedProduct NormalizeLine(string line)
        => new(
            NameExtractor.Extract(line),
            new Dictionary<string, string>()
            {
                { "Mass", GetUnit(line, "г").ToString() },
                { "Pieces", GetUnit(line, "шт").ToString() },
            });

    private static long GetUnit(string line, string measure)
    {
        var units = UnitExtractor.Extract(line)
            .Select(UnitNormalizer.Normalize)
            .ToArray();
        return UnitExtractor.GetUnit(units, measure);
    }
}
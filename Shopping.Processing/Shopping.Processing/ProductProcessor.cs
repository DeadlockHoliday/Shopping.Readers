using Shopping.Processing.Names;
using Shopping.Processing.Units;
using Shopping.Readers.Common.Data.Products;

namespace Shopping.Processing;

public static class ProductProcessor
{
    public static ProcessedProduct Process(UnprocessedProduct product)
        => new(
            NameExtractor.Extract(product.Info) ?? product.CategoryName,
            new Dictionary<string, string>()
            {
                { "Mass", GetUnit(product.Info, "г").ToString() },
                { "Pieces", GetUnit(product.Info, "шт").ToString() },
            });

    private static long GetUnit(string line, string measure)
    {
        var units = UnitExtractor.Extract(line)
            .Select(UnitNormalizer.Normalize)
            .ToArray();

        return UnitExtractor.GetUnit(units, measure);
    }
}
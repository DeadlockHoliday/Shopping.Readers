using Shopping.Processing.Products.Names;
using Shopping.Processing.Products.Units;
using Shopping.Common.Data.Products;

namespace Shopping.Processing.Products;

public static class ProductProcessor
{
    public static ProcessedProduct Process(Product product)
        => new(
            ProcessProduct(product),
            new Dictionary<string, string>()
            {
                { "Mass", GetUnit(product.Info, "г").ToString() },
                { "Pieces", GetUnit(product.Info, "шт").ToString() },
            });

    private static Product ProcessProduct(Product product)
        => new(product.Info, NameExtractor.Extract(product.Info) ?? product.Category);

    private static long GetUnit(string line, string measure)
    {
        var units = UnitExtractor.Extract(line)
            .Select(UnitNormalizer.Normalize)
            .ToArray();

        return UnitExtractor.GetUnit(units, measure);
    }
}
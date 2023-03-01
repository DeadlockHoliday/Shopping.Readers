using Shopping.Processing.Products.Names;
using Shopping.Processing.Products.Units;
using Shopping.Common.Data.Products;
using Shopping.Common.Modules;
using System.Text.Json;

namespace Shopping.Processing.Products.Facade;

public sealed class ProductProcessor : IProductProcessor
{
    public ProcessedProduct Process(Product product)
    {
        var details = new
        {
            Mass = GetUnit(product.Info, "г").ToString(),
            Pieces = GetUnit(product.Info, "шт").ToString(),
        };

        return new(
            ProcessProduct(product),
            JsonSerializer.Serialize(details));
    }

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
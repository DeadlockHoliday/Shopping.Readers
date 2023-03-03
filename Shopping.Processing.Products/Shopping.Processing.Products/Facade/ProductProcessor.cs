using Shopping.Processing.Products.Names;
using Shopping.Processing.Products.Units;
using Shopping.Common.Data.Products;
using Shopping.Common.Modules;
using Shopping.Common.Data.Features;

namespace Shopping.Processing.Products.Facade;

public sealed class ProductProcessor : IProductProcessor
{
    public Product Process(Product product)
    {
        var features = new FeatureSet(
            new CapacityFeatureSet
            {
                MassGramms = GetUnit(product.Info, "г"),
                Pieces = GetUnit(product.Info, "шт"),
            },
            new NameFeatureSet
            {
                GroupingName = NameExtractor.Extract(product.Info)
            }
        );

        return product with { ProcessorVersion = "v0.0001a", FeatureSet = features };
    }

    private static long GetUnit(string line, string measure)
    {
        var units = UnitExtractor.Extract(line)
            .Select(UnitNormalizer.Normalize)
            .ToArray();

        return UnitExtractor.GetUnit(units, measure);
    }
}
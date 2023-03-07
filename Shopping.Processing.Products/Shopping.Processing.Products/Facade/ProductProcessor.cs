using Shopping.Processing.Products.Names;
using Shopping.Processing.Products.Units;
using Shopping.Common.Modules;
using Shopping.Common.Data;
using System.Text.Json.Nodes;
using Shopping.Common.Data.Scoping;

namespace Shopping.Processing.Products.Facade;

public sealed class ProductProcessor : IProductProcessor
{
    public Product Process(Product product)
    {
        var sharedFeatureSet = new Dictionary<string, JsonNode>();

        _ = new CapacityFeatureScope(sharedFeatureSet)
        {
            MassGramms = GetUnit(product.Info, "г"),
            Pieces = GetUnit(product.Info, "шт"),
        };

        _ = new NamingFeatureScope(sharedFeatureSet)
        {
            GroupingName = NameExtractor.Extract(product.Info)
        };

        return product with 
        {
            ProcessorVersion = "v0.0001a", 
            Features = sharedFeatureSet 
        };
    }

    private static long GetUnit(string line, string measure)
    {
        var units = UnitExtractor.Extract(line)
            .Select(UnitNormalizer.Normalize)
            .ToArray();

        return UnitExtractor.GetUnit(units, measure);
    }
}
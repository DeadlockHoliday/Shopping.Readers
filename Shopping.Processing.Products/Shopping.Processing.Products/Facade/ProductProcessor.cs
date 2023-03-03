﻿using Shopping.Processing.Products.Names;
using Shopping.Processing.Products.Units;
using Shopping.Common.Data.Products;
using Shopping.Common.Modules;
using Shopping.Common.Data.Features;
using Shopping.Common.Data.Features.Wrappers;
using System.Text.Json.Nodes;

namespace Shopping.Processing.Products.Facade;

public sealed class ProductProcessor : IProductProcessor
{
    public Product Process(Product product)
    {
        var sharedFeatureSet = new JsonObject();

        _ = new CapacityFeatureSetWrapper(sharedFeatureSet)
        {
            MassGramms = GetUnit(product.Info, "г"),
            Pieces = GetUnit(product.Info, "шт"),
        };

        _ = new NameFeatureSetWrapper(sharedFeatureSet)
        {
            GroupingName = NameExtractor.Extract(product.Info)
        };

        return product with 
        {
            ProcessorVersion = "v0.0001a", 
            FeatureSet = sharedFeatureSet 
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
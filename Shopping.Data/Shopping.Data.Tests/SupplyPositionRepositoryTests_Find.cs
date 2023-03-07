﻿using LiteDB;
using NMoneys;
using Shopping.Common.Data;
using Shopping.Common.Data.Scoping;
using Shopping.Common.Data.Supply;
using Shopping.Common.Static;
using System.Collections.Immutable;
using System.Text.Json.Nodes;

#pragma warning disable CS8618

namespace Shopping.Data.Tests;

[TestFixture]
internal class SupplyPositionRepositoryTests_Find
{
    private IDatabaseManager databaseManager;

    [SetUp]
    public void Setup()
    {
        var stream = new MemoryStream();
        databaseManager = new DatabaseManager(stream);
        Seed();
    }

    [Test]
    public void Find_ShouldReturn_SingleItem()
    {
        using var connection = databaseManager.OpenDatabaseConnection();
        var result = connection.GetCollection<SupplyPosition>()
            .Find(x => x.Product.Info == "MiscProduct");

        Assert.That(result, Has.One.Items);
        Assert.That(result.First().Product.Info, Is.EqualTo("MiscProduct"));
    }

    [Test]
    public void Find_ShouldReturn_MultipleItems()
    {
        using var connection = databaseManager.OpenDatabaseConnection();
        var result = connection.GetCollection<SupplyPosition>()
            .Find(x => x.Product.Info.Equals("MiscProduct"));

        Assert.That(result.Count(), Is.GreaterThan(0));
    }

    private void Seed()
    {
        var productFeatures = new Dictionary<string, JsonNode>();
        _ = new CapacityFeatureScope(productFeatures)
        {
            MassGramms = 100,
            Pieces = 10,
        };

        var product1 = new Product()
        {
            Category = "Misc",
            Info = "MiscProduct",
            Features = productFeatures.ToImmutableDictionary(),
        };

        ////////////////////////////////////////////////////////

        productFeatures = new Dictionary<string, JsonNode>();
        _ = new CapacityFeatureScope(productFeatures)
        {
            MassGramms = 400,
            Pieces = 1,
        };

        _ = new NamingFeatureScope(productFeatures)
        {
            GroupingName = "Big"
        };

        var product2 = new Product()
        {
            Category = "Special",
            Info = "SpecialProduct",
            Features = productFeatures.ToImmutableDictionary()
        };

        ////////////////////////////////////////////////////////

        var supplyPositions = CreateSupplyPackage(product1, product2);
        using var connection = databaseManager.OpenDatabaseConnection();
        connection.GetCollection<SupplyPosition>().InsertBulk(supplyPositions);
    }

    private SupplyPosition[] CreateSupplyPackage(params Product[] products)
    {
        var invoice = new Invoice()
        {
            Date = DateOnly.FromDateTime(DateTime.Now.Date),
            Price = new Money(100),
            Quantity = 1,
            Vendor = Vendor.Walk,
        };

        return products
            .Select(p => new SupplyPosition()
            {
                Invoice = invoice,
                Product = p
            }).ToArray();
    }
}

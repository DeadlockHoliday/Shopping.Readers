using LiteDB;
using NMoneys;
using Shopping.Common.Data;
using Shopping.Common.Data.Scoping;
using Shopping.Common.Data.Supply;
using System.Text.Json.Nodes;

namespace Shopping.Data.Tests;

[TestFixture]
internal class SupplyPositionRepositoryTests
{
    private ISupplyPositionRepository repository;
    private IDatabaseManager databaseManager;

    [SetUp]
    public void Setup()
    {
        var stream = new MemoryStream();
        databaseManager = new DatabaseManager(stream);
        repository = new SupplyPositionRepository(databaseManager);
    }

    [Test]
    public void Add_ShouldUpdate_Collection()
    {
        AssertCount(0);
        var supplyPosition = Create();
        repository.Add(supplyPosition);
        AssertCount(1);
    }

    [Test]
    public void Update_ShouldUpdate_Collection()
    {
        AssertCount(0);

        var supplyPosition = Create();
        var id  = repository.Add(supplyPosition);

        var expectedProduct = new Product() { Info = "Updated_Info", Category = "Category" };
        var updatedSupplyPosition = Create() with
        {
            Id = id,
            Invoice = new(),
            Product = expectedProduct
        };

        repository.Update(updatedSupplyPosition);

        using var connection = databaseManager.OpenDatabaseConnection();
        var result = connection.GetCollection<SupplyPosition>().FindById(id).Product;
        Assert.That(result, Is.EqualTo(expectedProduct));
    }

    [Test]
    public void Remove_ShouldUpdate_Collection()
    {
        AssertCount(0);

        var supplyPosition = Create();
        var id = repository.Add(supplyPosition);

        AssertCount(1);

        repository.Remove(id);

        AssertCount(0);
    }

    private SupplyPosition Create()
    {
        var productFeatures = new JsonObject();
        _ = new CapacityFeatureScope(productFeatures)
        {
            MassGramms = 100,
            Pieces = 10,
        };

        _ = new NamingFeatureScope(productFeatures)
        {
            GroupingName = "Tasty"
        };

        var supplyPosition = new SupplyPosition()
        {
            Invoice = new Invoice()
            {
                Date = DateOnly.FromDateTime(DateTime.Now.Date),
                Price = new Money(100),
                Quantity = 1,
                Vendor = Shopping.Common.Static.Vendor.Walk,
            },
            Product = new Shopping.Common.Data.Product()
            {
                Category = "foo",
                Info = "bar",
                Features = productFeatures
            },
        };

        return supplyPosition;
    }

    private void AssertCount(int count)
    {
        using var connection = databaseManager.OpenDatabaseConnection();
        var actualCount = connection.GetCollection<SupplyPosition>().Count();
        Assert.That(actualCount, Is.EqualTo(count));
    }
}

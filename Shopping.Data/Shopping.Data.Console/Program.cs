using LiteDB;
using NMoneys;
using Shopping.Common.Data.Scoping;
using Shopping.Common.Data.Supply;
using Shopping.Data;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

////////////////////////////////////////////////////////////////////////////////////////////////
BsonMapper.Global.RegisterType<Money>
        (
            serialize: (money) => money.Amount,
            deserialize: (bson) => new Money(bson.AsDecimal)
        );

BsonMapper.Global.RegisterType<JsonObject>
(
            serialize: (jsonObject) => jsonObject.ToJsonString(),
            deserialize: (bson) => (JsonObject)JsonNode.Parse(bson.AsString)!
        );
////////////////////////////////////////////////////////////////////////////////////////////////

var connectionString = Environment.CurrentDirectory + @"\MyData.db";
File.Delete(connectionString);

var repository = new SupplyPositionRepository(connectionString);

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

repository.Add(new SupplyPosition()
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
});

var supply = repository.Get("");
var name = new NamingFeatureScope(supply.Product.Features).GroupingName;
var pieces = new CapacityFeatureScope(supply.Product.Features).Pieces;

Console.WriteLine("Hello, World!");

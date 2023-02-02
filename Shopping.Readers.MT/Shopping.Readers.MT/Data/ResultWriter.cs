using LiteDB;

namespace Shopping.Readers.MT.Data;

internal static class ResultWriter
{
    internal static void WriteResult(IEnumerable<Product> items, string writePath)
    {
        BsonMapper.Global.RegisterType
        (
            serialize: obj =>
            {
                return new BsonDocument()
                {
                    ["Year"] = obj.Year,
                    ["Month"] = obj.Month,
                    ["Day"] = obj.Day
                };
            },
            deserialize: doc =>
                new DateOnly(doc["Year"], doc["Month"], doc["Day"])
        );

        using (var db = new LiteDatabase(writePath))
        {
            db.DropCollection("orderItems");
            db.Commit();
        }

        using (var db = new LiteDatabase(writePath))
        {
            var collection = db.GetCollection<Product>("orderItems");
            foreach (var item in items)
            {
                collection.Insert(item);
            }
            db.Commit();
        }

        using (var db = new LiteDatabase(writePath))
        {
            var collection = db.GetCollection<Product>("orderItems");
            var one = collection.FindOne(x => true);
        }

        using (var db = new LiteDatabase(writePath))
        {
            var collection = db.GetCollection<Product>("orderItems");
            collection.EnsureIndex(x => x.OrderDate);
            collection.EnsureIndex(x => x.CategoryName);
            collection.EnsureIndex(x => x.FullName);
        }
    }
}

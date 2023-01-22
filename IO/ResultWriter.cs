﻿using LiteDB;

internal static class ResultWriter
{
    internal static void WriteResult(IEnumerable<OrderItem> items, string writePath)
    {
        BsonMapper.Global.RegisterType
        (
            serialize: obj =>
            {
                return new()
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
            var collection = db.GetCollection<OrderItem>("orderItems");
            foreach (var item in items)
            {
                collection.Insert(item);
            }
            db.Commit();
        }

        using (var db = new LiteDatabase(writePath))
        {
            var collection = db.GetCollection<OrderItem>("orderItems");
            var one = collection.FindOne(x => true);
        }

        using (var db = new LiteDatabase(writePath))
        {
            var collection = db.GetCollection<OrderItem>("orderItems");
            collection.EnsureIndex(x => x.OrderDate);
            collection.EnsureIndex(x => x.CategoryName);
            collection.EnsureIndex(x => x.ProductName);
        }
    }
}

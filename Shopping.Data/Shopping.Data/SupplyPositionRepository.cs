using LiteDB;
using Shopping.Common.Data.Supply;
using Shopping.Common.Modules;

namespace Shopping.Data;

public interface ISupplyPositionRepository
{
    void Add(SupplyPosition position);
    void Update(SupplyPosition position);
    void Remove(SupplyPosition position);
    SupplyPosition Get(string jsonPath);
    SupplyPosition[] Load(string jsonPath);
}

public class SupplyPositionRepository : ISupplyPositionRepository
{
    private readonly string connectionString;

    public SupplyPositionRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void Add(SupplyPosition position)
    {
        using var db = new LiteDatabase(connectionString);
        var collection = db.GetCollection<SupplyPosition>();
        collection.Insert(position);
    }

    public SupplyPosition Get(string jsonPath)
    {
        if (!string.IsNullOrWhiteSpace(jsonPath))
        {
            throw new NotImplementedException("jsonPath is under development");
        }

        using var db = new LiteDatabase(connectionString);
        var collection = db.GetCollection<SupplyPosition>();
        return collection.Query().First();
    }

    public SupplyPosition[] Load(string jsonPath)
    {
        if (!string.IsNullOrWhiteSpace(jsonPath))
        {
            throw new NotImplementedException("jsonPath is under development");
        }

        using var db = new LiteDatabase(connectionString);
        var collection = db.GetCollection<SupplyPosition>();
        return collection.Query().ToArray();
    }

    public void Remove(SupplyPosition position)
    {
        using var db = new LiteDatabase(connectionString);
        var collection = db.GetCollection<SupplyPosition>();
        // collection.Delete()
        throw new NotImplementedException("No Id Provided.");
    }

    public void Update(SupplyPosition position)
    {
        using var db = new LiteDatabase(connectionString);
        var collection = db.GetCollection<SupplyPosition>();
        // collection.Update()
        throw new NotImplementedException("No Id Provided.");
    }
}
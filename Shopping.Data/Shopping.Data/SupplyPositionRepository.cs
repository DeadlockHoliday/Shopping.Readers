using Shopping.Common.Data.Supply;

namespace Shopping.Data;

public class SupplyPositionRepository : ISupplyPositionRepository
{
    private readonly IDatabaseManager databaseManager;

    public SupplyPositionRepository(IDatabaseManager databaseManager)
    {
        BsonMapperRegistry.Register();
        this.databaseManager = databaseManager;
    }

    public Guid Add(SupplyPosition position)
    {
        using var db = databaseManager.OpenDatabaseConnection();
        var collection = db.GetCollection<SupplyPosition>();
        return collection.Insert(position);
    }

    public SupplyPosition Get(string jsonPath)
    {
        if (!string.IsNullOrWhiteSpace(jsonPath))
        {
            throw new NotImplementedException("jsonPath is under development");
        }

        using var db = databaseManager.OpenDatabaseConnection();
        var collection = db.GetCollection<SupplyPosition>();
        return collection.Query().First();
    }

    public SupplyPosition[] Load(string jsonPath)
    {
        if (!string.IsNullOrWhiteSpace(jsonPath))
        {
            throw new NotImplementedException("jsonPath is under development");
        }

        using var db = databaseManager.OpenDatabaseConnection();
        var collection = db.GetCollection<SupplyPosition>();
        return collection.Query().ToArray();
    }

    public void Remove(Guid id)
    {
        using var db = databaseManager.OpenDatabaseConnection();
        var collection = db.GetCollection<SupplyPosition>();
        collection.Delete(id);
    }

    public bool Update(SupplyPosition position)
    {
        using var db = databaseManager.OpenDatabaseConnection();
        var collection = db.GetCollection<SupplyPosition>();
        return collection.Update(position);
    }
}
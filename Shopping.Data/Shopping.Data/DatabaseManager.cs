using LiteDB;

namespace Shopping.Data;

public class DatabaseManager : IDatabaseManager
{
    private readonly string? connectionString;
    private readonly Stream? stream;

    static DatabaseManager()
    {
        BsonMapperRegistry.Register();
    }

    public DatabaseManager(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public DatabaseManager(Stream stream)
    {
        this.stream = stream;
    }

    public ILiteDatabase OpenDatabaseConnection() 
    {
        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            return new LiteDatabase(connectionString);
        }
        else if (stream != null) 
        {
            return new LiteDatabase(stream);
        }

        throw new InvalidOperationException();
    }
}

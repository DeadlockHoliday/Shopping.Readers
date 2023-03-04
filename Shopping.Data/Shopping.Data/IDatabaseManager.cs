using LiteDB;

namespace Shopping.Data;

public interface IDatabaseManager
{
    ILiteDatabase OpenDatabaseConnection();
}

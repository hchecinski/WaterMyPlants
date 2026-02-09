using Microsoft.Data.Sqlite;
using System.Data;

namespace WaterMyPlants.Infrastructure.DataBase;

public class SqliteConnectionFactory : ISqliteConnectionFactory
{
    private readonly string _dbPath;

    public SqliteConnectionFactory(string dbPath)
    {
        _dbPath = dbPath;
    }

    public IDbConnection Create() => new SqliteConnection($"Data Source={_dbPath}");
}
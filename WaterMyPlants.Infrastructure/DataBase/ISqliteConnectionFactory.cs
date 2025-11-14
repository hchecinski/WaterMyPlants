using System.Data;

namespace WaterMyPlants.Infrastructure.DataBase;

public interface ISqliteConnectionFactory
{
    IDbConnection Create();
}

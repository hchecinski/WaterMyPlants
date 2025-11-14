using Dapper;
using System.Diagnostics;
using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;
using WaterMyPlants.Infrastructure.DataBase;

namespace WaterMyPlants.Infrastructure.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly ISqliteConnectionFactory _factory;

    public PhotoRepository(ISqliteConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task InsertAsync(Photo entity)
    {
        try
        {
            using var conn = _factory.Create();

            var sql = @"INSERT INTO Photos (Id, PlantId, Path, CreatedAt) VALUES (@Id, @PlantId, @Path, @CreatedAt);";

            await conn.ExecuteAsync(sql, entity);
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            using var conn = _factory.Create();

            var sql = @"DELETE FROM Photos WHERE Id = @Id;";

            await conn.ExecuteAsync(sql, new { Id = id });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
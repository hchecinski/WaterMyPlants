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

            var sql = @"INSERT INTO Photos (Id, PlantId, Path, CreatedAt, Name, LastUpdatedAt) VALUES (@Id, @PlantId, @Path, @CreatedAt, @Name, @LastUpdatedAt);";

            await conn.ExecuteAsync(sql, entity);
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
            throw new Exception();
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            using var conn = _factory.Create();

            var sql = @"DELETE FROM Photos WHERE Id = @Id;";

            await conn.ExecuteAsync(sql, new { Id = id.ToString() });

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return false;
    }

    public async Task<bool> UpdateAsync(Photo entity)
    {
        try
        {
            using var conn = _factory.Create();

            var sql = @"UPDATE Photos
                        SET 
                            Name = @Name,
                            LastUpdatedAt = @LastUpdatedAt
                        WHERE Id = @Id;";

            await conn.ExecuteAsync(sql, entity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return false;
    }

    public async Task<IEnumerable<Photo>> GetAllAsync(Guid plantId)
    {
        try
        {
            using var conn = _factory.Create();

            var sql = @"SELECT * FROM Photos WHERE PlantId = @PlantId;";

            return await conn.QueryAsync<Photo>(sql, new { PlantId = plantId.ToString() });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Enumerable.Empty<Photo>();
        }
    }

    public async Task<Photo?> GetAsync(Guid photoId)
    {
        try
        {
            using var conn = _factory.Create();

            var sql = @"SELECT * FROM Photos WHERE Id = @Id;";

            return await conn.QuerySingleOrDefaultAsync<Photo>(sql, new { Id = photoId.ToString() });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
    }
}
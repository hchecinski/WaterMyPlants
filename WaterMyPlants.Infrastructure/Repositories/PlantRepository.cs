using Dapper;
using System.Diagnostics;
using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;
using WaterMyPlants.Infrastructure.DataBase;
using static Dapper.SqlMapper;

namespace WaterMyPlants.Infrastructure.Repositories;

public class PlantRepository : IPlantRepository
{
    private readonly ISqliteConnectionFactory _factory;

    public PlantRepository(ISqliteConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<Plant>> GetAllAsync()
    {
        try
        {
            using var conn = _factory.Create();

            const string sqlPlant = "SELECT * FROM Plants;";
            const string sqlNotes = "SELECT * FROM Notes;";
            const string sqlPhotos = "SELECT * FROM Photos;";

            var plants = await conn.QueryAsync<Plant>(sqlPlant);
            if (plants is null || !plants.Any())
            {
                return Enumerable.Empty<Plant>();
            }

            var notes = await conn.QueryAsync<Note>(sqlNotes);
            var photos = await conn.QueryAsync<Photo>(sqlPhotos);

            foreach (var plant in plants)
            {
                var plantNotes = notes.Where(n => n.PlantId == plant.Id).ToList();
                plant.ReplaceNotes(plantNotes);

                var plantPhotos = photos.Where(p => p.PlantId == plant.Id).ToList();
                plant.ReplacePhotos(plantPhotos);
            }

            return plants;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Enumerable.Empty<Plant>();
        }
    }

    public async Task<Plant?> GetAsync(Guid id)
    {
        try
        {
            using var conn = _factory.Create();

            const string sql = """
                SELECT * FROM Plants WHERE Id = @Id;

                SELECT * FROM Notes WHERE PlantId = @Id ORDER BY CreatedAt;

                SELECT * FROM Photos WHERE PlantId = @Id ORDER BY CreatedAt;
            """;

            using var multi = await conn.QueryMultipleAsync(sql, new { Id = id.ToString() });

            var plant = await multi.ReadSingleOrDefaultAsync<Plant>();
            if (plant is null)
            {
                return null;
            }

            var notes = (await multi.ReadAsync<Note>()).ToList();
            plant.ReplaceNotes(notes);

            var photos = (await multi.ReadAsync<Photo>()).ToList();
            plant.ReplacePhotos(photos);

            return plant;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task InsertAsync(Plant entity)
    {
        try
        {
            using var conn = _factory.Create();

            conn.Open();
            if (conn.State != System.Data.ConnectionState.Open)
            {
                throw new InvalidOperationException("Database connection is not open.");
            }

            const string sql = """
                INSERT INTO Plants (Id, Name, Localization, Description, WaterIntervalDays, CreatedAt, LastWaterAt, LastUpdatedAt)
                VALUES (@Id, @Name, @Localization, @Description, @WaterIntervalDays, @CreatedAt, @LastWaterAt, @LastUpdatedAt);
            """;

            await conn.ExecuteAsync(sql, new
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Localization = entity.Localization,
                Description = entity.Description,
                WaterIntervalDays = entity.WaterIntervalDays,
                CreatedAt = entity.CreatedAt,
                LastWaterAt = entity.LastWaterAt,
                LastUpdatedAt = entity.LastUpdatedAt
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public async Task<bool> UpdateAsync(Plant entity)
    {
        try
        {
            using var conn = _factory.Create();

            const string sql = """
                UPDATE Plants
                SET 
                    Name = @Name,
                    Description = @Description,
                    Localization = @Localization,
                    WaterIntervalDays = @WaterIntervalDays,
                    LastUpdatedAt = @LastUpdatedAt 
                WHERE Id = @Id;
            """;

            await conn.ExecuteAsync(sql, entity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return false;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            using var conn = _factory.Create();

            const string sql = """
                DELETE FROM Plants WHERE Id = @Id;
            """;

            await conn.ExecuteAsync(sql, new { Id = id.ToString() });

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return false;
    }

    public async Task WaterAsync(Guid id, DateTime nowUtc)
    {
        try
        {
            using var conn = _factory.Create();

            const string sql = """
                UPDATE Plants
                SET 
                    LastWaterAt = @NowUtc
                WHERE Id = @Id;
            """;

            DateTime? lastWatered = nowUtc;

            await conn.ExecuteAsync(sql, new { Id = id, NowUtc = lastWatered });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public Task<Plant> GetReadOnlyAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}

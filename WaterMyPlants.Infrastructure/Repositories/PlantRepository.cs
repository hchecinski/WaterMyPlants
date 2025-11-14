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

            const string sql = """
                SELECT 
                    p.Id, p.Name, p.Localization, p.Description,
                    p.WaterIntervalDays, p.CreatedAt, p.LastWaterAt, p.LastUpdatedAt,
                    n.Id, n.PlantId, n.Text, n.CreatedAt
                FROM Plants p
                LEFT JOIN Notes n ON n.PlantId = p.Id
                LEFT JOIN Photos ph ON ph.PlantId = p.Id
                ORDER BY p.CreatedAt;
            """;

            var plantDictionary = new Dictionary<Guid, Plant>();

            await conn.QueryAsync<Plant, Note, Photo, Plant>(
                sql,
                (plant, note, photo) =>
                {
                    if (!plantDictionary.TryGetValue(plant.Id, out var existing))
                    {
                        existing = plant;
                        plantDictionary.Add(existing.Id, existing);
                    }

                    if (note != null)
                    {
                        existing.AddNote(note);
                    }

                    if(photo != null)
                    {
                        existing.AddPhoto(photo);
                    }
                    return existing;
                }
            );

            return plantDictionary.Values;
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
            """;

            using var multi = await conn.QueryMultipleAsync(sql, new { Id = id });

            var plant = await multi.ReadSingleOrDefaultAsync<Plant>();
            if (plant is null)
            {
                return null;
            }

            var notes = (await multi.ReadAsync<Note>()).ToList();
            plant.CreateNotes(notes);

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

            const string sql = """
                INSERT INTO Plants (Id, Name, Localization, Description, WaterIntervalDays, CreatedAt, LastWaterAt, LastUpdatedAt)
                VALUES (@Id, @Name, @Localization, @Description, @WaterIntervalDays, @CreatedAt, @LastWaterAt, @LastUpdatedAt);
            """;

            await conn.ExecuteAsync(sql, entity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public async Task UpdateAsync(Plant entity)
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
                    LastUpdatedAt = @LastUpdatedAt,
                WHERE Id = @Id;
            """;

            await conn.ExecuteAsync(sql, entity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            using var conn = _factory.Create();

            const string sql = """
                DELETE FROM Plants WHERE Id = @Id;
            """;

            await conn.ExecuteAsync(sql, new { Id = id });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public async Task WaterAsync(Guid id, DateTime nowUtc)
    {
        try
        {
            using var conn = _factory.Create();

            const string sql = """
                UPDATE Plants
                SET 
                    LastWaterAt = @NowUtc,
                WHERE Id = @Id;
            """;

            await conn.ExecuteAsync(sql, new { Id = id, NowUtc = nowUtc });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}

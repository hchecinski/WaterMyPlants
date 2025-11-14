using Dapper;
using System.Diagnostics;
using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;
using WaterMyPlants.Infrastructure.DataBase;

namespace WaterMyPlants.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    ISqliteConnectionFactory _connectionFactory;

    public NoteRepository(ISqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InsertAsync(Note entity)
    {
        try
        {
            using var conn = _connectionFactory.Create();

            var sql = @"INSERT INTO Notes (Id, PlantId, Text, CreatedAt)
                        VALUES (@Id, @PlantId, @Text, @CreatedAt);";

            await conn.ExecuteAsync(sql, entity);

        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public async Task UpdateAsync(Note entity)
    {
        try
        {
            using var conn = _connectionFactory.Create();

            var sql = @"UPDATE Notes
                        SET 
                            Text = @Text,
                            UpdatedAt = @UpdatedAt
                        WHERE Id = @Id;";

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
            using var conn = _connectionFactory.Create();

            var sql = @"DELETE FROM Notes WHERE Id = @Id;";

            await conn.ExecuteAsync(sql, new { Id = id });
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}

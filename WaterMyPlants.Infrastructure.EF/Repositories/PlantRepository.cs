using Microsoft.Extensions.Logging;
using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;

namespace WaterMyPlants.Infrastructure.EF.Repositories;

public class PlantRepository : IPlantRepository
{
    private readonly ILogger<PlantRepository> _logger;

    public PlantRepository(ILogger<PlantRepository> logger)
    {
        _logger = logger;
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Plant>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Plant?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(Plant entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Plant entity)
    {
        throw new NotImplementedException();
    }

    public Task WaterAsync(Guid id, DateTime nowUtc)
    {
        throw new NotImplementedException();
    }
}

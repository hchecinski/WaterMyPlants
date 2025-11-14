using WaterMyPlants.Domain.Models;

namespace WaterMyPlants.Domain.Repositories;

public interface IPlantRepository : IUpdatableRepository<Plant>
{
    Task<Plant?> GetAsync(Guid id);
    Task<IEnumerable<Plant>> GetAllAsync();
    Task WaterAsync(Guid id, DateTime nowUtc);
}

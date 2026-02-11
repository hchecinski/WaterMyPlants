using WaterMyPlants.Domain.Models;

namespace WaterMyPlants.Domain.Repositories;

public interface IPlantRepository 
{
    Task<Plant?> GetAsync(Guid id);
    Task<IReadOnlyList<Plant>> GetAllAsync();
    Task AddAsync(Plant plant);
    Task RemoveAsync(Plant plant);
    Task SaveAsync();
}

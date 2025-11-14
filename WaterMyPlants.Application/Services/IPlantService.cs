using WaterMyPlants.Application.Models;

namespace WaterMyPlants.Application.Services;

public interface IPlantService
{
    Task<Guid> AddAsync(PlantDto plant);
    Task UpdateAsync(PlantDto plant);
    Task DeleteAsync(Guid id);
    Task WaterNowAsync(Guid id, DateTime nowUtc);
    Task UndoWaterAsync(Guid id, DateTime previousUtc);
    Task<IReadOnlyList<PlantListItemDto>> GetSortedAsync();
    Task<PlantDetailsDto> GetDetailsAsync(Guid id);
}

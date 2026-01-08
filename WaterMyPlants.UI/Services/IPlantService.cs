using WaterMyPlants.Shared.Models;

namespace WaterMyPlants.UI.Services;

public interface IPlantService
{
    Task<Guid> AddAsync(CreatePlantDto createPlantDto);
    Task UpdateAsync(UpdatePlantDto plantDto);
    Task DeleteAsync(Guid id);
    Task WaterNowAsync(Guid id);
    Task UndoWaterAsync(Guid id, DateTime previousUtc);
    Task<IReadOnlyList<PlantListItemDto>> GetSortedAsync();
    Task<PlantDetailsDto> GetDetailsAsync(Guid id);
    Task<UpdatablePlantDto> GetUpdatablePlantAsync(Guid value);
}

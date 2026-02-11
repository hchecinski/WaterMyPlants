using WaterMyPlants.Shared.Dtos;

namespace WaterMyPlants.Application.Services;

public interface IPlantService
{
    Task<Guid> AddAsync(CreatePlantDto createPlantDto);
    Task UpdateAsync(Guid id, UpdatePlantDto plantDto);
    Task DeleteAsync(Guid id);
    Task WaterNowAsync(Guid id);
    Task UndoWaterAsync(Guid id, DateTime previousUtc);
    Task<IReadOnlyList<PlantListItemDto>> GetSortedAsync();
    Task<PlantDetailsDto> GetDetailsAsync(Guid id);
    Task<UpdatablePlantDto> GetUpdatablePlantAsync(Guid value);
}

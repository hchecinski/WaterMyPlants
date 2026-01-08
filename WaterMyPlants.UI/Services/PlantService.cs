using WaterMyPlants.Shared.Models;

namespace WaterMyPlants.UI.Services;

public class PlantService : IPlantService
{
    public Task<Guid> AddAsync(CreatePlantDto createPlantDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PlantDetailsDto> GetDetailsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<PlantListItemDto>> GetSortedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UpdatablePlantDto> GetUpdatablePlantAsync(Guid value)
    {
        throw new NotImplementedException();
    }

    public Task UndoWaterAsync(Guid id, DateTime previousUtc)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdatePlantDto plantDto)
    {
        throw new NotImplementedException();
    }

    public Task WaterNowAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}

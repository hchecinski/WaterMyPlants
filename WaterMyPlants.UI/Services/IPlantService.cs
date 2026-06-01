
using WaterMyPlants.UI.Models;

namespace WaterMyPlants.UI.Services;

public interface IPlantService
{
    Task<Guid> AddAsync(CreatePlantModel plant);
    Task UpdateAsync(UpdatePlantModel plant);
    Task DeleteAsync(Guid id);
    Task WaterNowAsync(Guid id);
    Task UndoWaterAsync(Guid id, DateTime previousUtc);
    Task<IReadOnlyList<PlantListItemModel>> GetSortedAsync();
    Task<PlantDetailsModel?> GetDetailsAsync(Guid id);
    Task<UpdatePlantModel> GetUpdatablePlantAsync(Guid value);
}

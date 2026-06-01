using WaterMyPlants.Connector.Connectors;
using WaterMyPlants.Shared.Dtos;
using WaterMyPlants.UI.Models;

namespace WaterMyPlants.UI.Services;

public class PlantService : IPlantService
{
    IPlantConnector _plantConnector;
    IMapper _mapper;

    public PlantService(IPlantConnector plantConnector, IMapper mapper)
    {
        _plantConnector = plantConnector;
        _mapper = mapper;
    }

    public async Task<PlantDetailsModel?> GetDetailsAsync(Guid id)
    {
        var plantDetailsDto = await _plantConnector.GetDetailsAsync(id);
        if(plantDetailsDto == null)
        {
            return null;
        }

        return  _mapper.ToModel(plantDetailsDto);
    }

    public async Task<IReadOnlyList<PlantListItemModel>> GetSortedAsync()
    {
        var plants = await _plantConnector.GetPlantsAsync();
        if(plants == null || !plants.Any())
        {
            return [];
        }

        return plants.Select(_mapper.ToModel).ToList();
    }
    
    public Task<UpdatePlantModel> GetUpdatablePlantAsync(Guid value)
    {
        throw new NotImplementedException();
    }


    public async Task<Guid> AddAsync(CreatePlantModel plant)
    {
        var dto = _mapper.ToDto(plant);
        var added =  await _plantConnector.AddPlantAsync(dto);
        return added?.Id ?? Guid.Empty;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _plantConnector.DeletePlantAsync(id);
    }

    public Task UndoWaterAsync(Guid id, DateTime previousUtc)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdatePlantModel plant)
    {
        throw new NotImplementedException();
    }

    public Task WaterNowAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}

using System.Diagnostics;
using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;
using WaterMyPlants.Shared.Models;

namespace WaterMyPlants.Application.Services;

public class PlantService : IPlantService
{
    private readonly IPlantRepository _plantRepository;
    private readonly IMapper _mapper;

    public PlantService(IPlantRepository plantRepository, IMapper mapper)
    {
        _plantRepository = plantRepository;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(CreatePlantDto createPlantDto)
    {
        Plant plant = new Plant(createPlantDto.Name, createPlantDto.WaterIntervalDays, createPlantDto.Localization, createPlantDto.Description);
        plant.CreateNew();

        await _plantRepository.InsertAsync(plant);

        return Guid.Parse(plant.Id);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _plantRepository.DeleteAsync(id);
    }

    public async Task<PlantDetailsDto> GetDetailsAsync(Guid id)
    {
        var plant = await _plantRepository.GetAsync(id);

        if (plant == null)
        {
            throw new KeyNotFoundException($"Plant with id '{id}' was not found.");
        }

        return _mapper.ToPlantDetailsDto(plant);
    }

    public async Task<IReadOnlyList<PlantListItemDto>> GetSortedAsync()
    {
        var plants = await _plantRepository.GetAllAsync();

        var plantsDto = plants.Select(_mapper.ToPlantListItemDto);

        return plantsDto.OrderByDescending(p => p.DaysRemaining).ToList();
    }

    public async Task<UpdatablePlantDto> GetUpdatablePlantAsync(Guid value)
    {
        var plant = await _plantRepository.GetAsync(value);

        if (plant == null)
        {
            throw new KeyNotFoundException($"Plant with id '{value}' was not found.");
        }

        plant.Validate();

        return _mapper.ToUpdatablePlantDto(plant);
    }

    public Task UndoWaterAsync(Guid id, DateTime previousUtc)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(UpdatePlantDto plantDto)
    {
        var plant = await _plantRepository.GetAsync(plantDto.Id);

        if (plant == null)
        {
            throw new KeyNotFoundException($"Plant with id '{plantDto.Id}' was not found.");
        }
        plant.Validate();
        plant.Update(plantDto.Name, plantDto.Description, plantDto.Localization, plantDto.WaterIntervalDays);

        await _plantRepository.UpdateAsync(plant);
    }

    public async Task WaterNowAsync(Guid id)
    {
        try
        {
            var plant = await _plantRepository.GetAsync(id);
            if (plant == null)
            {
                throw new KeyNotFoundException($"Plant with id '{id}' was not found.");
            }

            plant.Validate();
            plant.Water();

            if(plant.LastWaterAt is null)
            {
                throw new Exception("LastWaterAt cannot be null after watering the plant.");
            }

            await _plantRepository.WaterAsync(Guid.Parse(plant.Id), plant.LastWaterAt.Value);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR watering plant {id}: {ex.Message}");
        }
    }
}

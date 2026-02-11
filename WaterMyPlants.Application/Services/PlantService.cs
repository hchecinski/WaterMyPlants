using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WaterMyPlants.Domain.Exceptions;
using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;
using WaterMyPlants.Shared.Dtos;

namespace WaterMyPlants.Application.Services;

public class PlantService : IPlantService
{
    private readonly ILogger<PlantService> _logger;
    private readonly IPlantRepository _plantRepository;
    private readonly IMapper _mapper;

    public PlantService(ILogger<PlantService> logger, IPlantRepository plantRepository, IMapper mapper)
    {
        _logger = logger;
        _plantRepository = plantRepository;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(CreatePlantDto createPlantDto)
    {
        Plant plant = Plant.Create(Guid.NewGuid(), createPlantDto.Name, createPlantDto.WaterIntervalDays, DateTime.UtcNow, createPlantDto.Localization, createPlantDto.Description);

        await _plantRepository.AddAsync(plant);

        return plant.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
        var plant = await _plantRepository.GetAsync(id);

        if (plant is null)
        {
            throw new NotFoundException($"Plant with id '{id}' was not found.");
        }
        await _plantRepository.RemoveAsync(plant);
    }

    public async Task<PlantDetailsDto> GetDetailsAsync(Guid id)
    {
        var plant = await _plantRepository.GetAsync(id);

        if (plant == null)
        {
            throw new NotFoundException($"Plant with id '{id}' was not found.");
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
            throw new NotFoundException($"Plant with id '{value}' was not found.");
        }

        return _mapper.ToUpdatablePlantDto(plant);
    }

    public Task UndoWaterAsync(Guid id, DateTime previousUtc)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Guid id, UpdatePlantDto plantDto)
    {
        var plant = await _plantRepository.GetAsync(id);

        if (plant == null)
        {
            throw new NotFoundException($"Plant with id '{id}' was not found.");
        }

        plant.Update(plantDto.Name, plantDto.WaterIntervalDays, DateTime.UtcNow, plantDto.Description, plantDto.Localization);
        await _plantRepository.SaveAsync();
    }

    public async Task WaterNowAsync(Guid id)
    {
        var plant = await _plantRepository.GetAsync(id);
        if (plant == null)
        {
            throw new KeyNotFoundException($"Plant with id '{id}' was not found.");
        }

        plant.Water(DateTime.UtcNow);

        if (plant.LastWaterAt is null)
        {
            throw new Exception("LastWaterAt cannot be null after watering the plant.");
        }

        await _plantRepository.SaveAsync();
    }
}

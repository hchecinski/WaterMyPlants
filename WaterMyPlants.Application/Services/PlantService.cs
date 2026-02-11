using Microsoft.Extensions.Logging;
using WaterMyPlants.Domain.Exceptions;
using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;
using WaterMyPlants.Shared.Dtos;

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
        Plant plant = Plant.Create(Guid.NewGuid(), createPlantDto.Name, createPlantDto.WaterIntervalDays, DateTime.UtcNow, createPlantDto.Localization, createPlantDto.Description);

        await _plantRepository.AddAsync(plant);

        return plant.Id;
    }

    public async Task<Guid> AddNoteAsync(Guid plantId, CreateNoteDto note)
    {
        var plant = await _plantRepository.GetAsync(plantId);
        if (plant == null)
        {
            throw new NotFoundException($"Plant with id '{plantId}' was not found.");
        }

        var noteId = Guid.NewGuid();
        plant.AddNote(noteId, DateTime.UtcNow, note.Text);
        await _plantRepository.AddNoteAsync(plant.Notes.FirstOrDefault(i => i.Id == noteId)!);

        return noteId;
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

    public async Task DeleteNoteAsync(Guid plantId, Guid id)
    {
        var plant = await _plantRepository.GetAsync(plantId);
        if (plant == null)
        {
            throw new NotFoundException($"Plant with id '{plantId}' was not found.");
        }

        plant.RemoveNote(id);
        await _plantRepository.SaveAsync();
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

    public async Task<NoteDto> GetNoteById(Guid plantId, Guid id)
    {
        var note = await _plantRepository.GetNoteByIdAsync(plantId, id);
        if(note == null)
        {
            throw new NotFoundException($"Note with id '{id}' for plant with id '{plantId}' was not found.");
        }

        return _mapper.ToNoteDto(note);
    }

    public async Task<IReadOnlyList<NoteDto>> GetNotesAsync(Guid plantId)
    {
        var notes =  await _plantRepository.GetNotesAsync(plantId);

        return notes.Select(_mapper.ToNoteDto).ToList();
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

    public async Task UpdateNoteAsync(Guid plantId, Guid id, UpdateNoteDto note)
    {
        var plant = await _plantRepository.GetAsync(plantId);
        if (plant == null)
        {
            throw new NotFoundException($"Plant with id '{plantId}' was not found.");
        }

        plant.UpdateNote(id, note.Text, DateTime.UtcNow);
        await _plantRepository.SaveAsync();
    }

    public async Task WaterNowAsync(Guid id)
    {
        var plant = await _plantRepository.GetAsync(id);
        if (plant == null)
        {
            throw new NotFoundException($"Plant with id '{id}' was not found.");
        }

        plant.Water(DateTime.UtcNow);
        await _plantRepository.SaveAsync();
    }
}

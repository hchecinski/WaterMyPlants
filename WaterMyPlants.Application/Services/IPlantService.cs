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

    Task<Guid> AddNoteAsync(Guid plantId, CreateNoteDto note);
    Task<NoteDto> GetNoteById(Guid plantId, Guid id);
    Task DeleteNoteAsync(Guid plantId, Guid id);
    Task UpdateNoteAsync(Guid plantId, Guid id, UpdateNoteDto note);
    Task<IReadOnlyList<NoteDto>> GetNotesAsync(Guid plantId);
}

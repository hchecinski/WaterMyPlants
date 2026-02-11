using WaterMyPlants.Domain.Models;

namespace WaterMyPlants.Domain.Repositories;

public interface IPlantRepository 
{
    Task AddAsync(Plant plant);
    Task RemoveAsync(Plant plant);
    Task SaveAsync();

    Task AddNoteAsync(Note note);

    Task<Plant?> GetAsync(Guid id);
    Task<IReadOnlyList<Plant>> GetAllAsync();

    Task<Note?> GetNoteByIdAsync(Guid plantId, Guid noteId);
    Task<IReadOnlyList<Note>> GetNotesAsync(Guid plantId);
}

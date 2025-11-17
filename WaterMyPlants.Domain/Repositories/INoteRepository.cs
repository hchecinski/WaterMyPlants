using WaterMyPlants.Domain.Models;

namespace WaterMyPlants.Domain.Repositories;

public interface INoteRepository : IUpdatableRepository<Note>
{
    Task<Note?> GetItemByIdAsync(Guid noteId);
    Task<IEnumerable<Note>> GetNotes(Guid plantId);
}

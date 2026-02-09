using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;

namespace WaterMyPlants.Infrastructure.EF.Repositories;

public class NoteRepository : INoteRepository
{
    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Note?> GetItemByIdAsync(Guid noteId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Note>> GetNotes(Guid plantId)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(Note entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Note entity)
    {
        throw new NotImplementedException();
    }
}

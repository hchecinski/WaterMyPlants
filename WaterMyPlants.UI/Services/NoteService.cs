using WaterMyPlants.Shared.Dtos;

namespace WaterMyPlants.UI.Services;

public class NoteService : INoteService
{
    public Task AddNoteAsync(Guid plantId, string text)
    {
        throw new NotImplementedException();
    }

    public Task DeleteNoteAsync(Guid noteId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<NoteDto>> GetNotes(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdataNoteAsync(Guid noteId, string text)
    {
        throw new NotImplementedException();
    }
}

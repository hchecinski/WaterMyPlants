using WaterMyPlants.Shared.Dtos;

namespace WaterMyPlants.UI.Services;

public interface INoteService
{
    Task AddNoteAsync(Guid plantId, string text);
    Task DeleteNoteAsync(Guid noteId);
    Task<IEnumerable<NoteDto>> GetNotes(Guid id);
    Task UpdataNoteAsync(Guid noteId, string text);
}

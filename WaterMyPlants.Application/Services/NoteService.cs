
using WaterMyPlants.Application.Models;
using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;

namespace WaterMyPlants.Application.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;

    public NoteService(INoteRepository noteRepository, IMapper mapper)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }

    public async Task AddNoteAsync(Guid plantId, string text)
    {
        Note note = new Note(Guid.NewGuid(), DateTime.UtcNow)
        {
            PlantId = plantId.ToString(),
            Text = text
        };

        await _noteRepository.InsertAsync(note);
    }

    public async Task DeleteNoteAsync(Guid noteId)
    {
        await _noteRepository.DeleteAsync(noteId);
    }

    public async Task<IEnumerable<NoteDto>> GetNotes(Guid id)
    {
        var notes = await _noteRepository.GetNotes(id);

        return notes.Select(_mapper.ToNoteDto);
    }

    public async Task UpdataNoteAsync(Guid noteId, string text)
    {
        Note? note = await _noteRepository.GetItemByIdAsync(noteId);
        if (note == null)
        {
            return;
        }

        note.Updated(text);
        await _noteRepository.UpdateAsync(note);
    }
}

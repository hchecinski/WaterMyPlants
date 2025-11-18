using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WaterMyPlants.Application.Services;
using WaterMyPlants.UI.Models;

namespace WaterMyPlants.UI.ViewModels;

public partial class NoteFormViewModel : ObservableObject
{
    private readonly IMapper _mapper;
    private readonly INoteService _noteService;

    private readonly Guid? _noteId;
    private readonly Guid _plantId;
    private readonly Func<Task> _refreshNotes;
    private readonly Action _cancel;

    [ObservableProperty]
    private string _noteText = string.Empty;

    public NoteFormViewModel(Guid plantId, Func<Task> refreshNotes, Action cancel, IMapper mapper, INoteService noteService)
    {
        _mapper = mapper;
        _noteService = noteService;
        _plantId = plantId;
        _refreshNotes = refreshNotes;
        _cancel = cancel;
    }

    public NoteFormViewModel(NoteModel note, Func<Task> refreshNotes, Action cancel, IMapper mapper, INoteService noteService)
    {
        _noteId = note.Id;
        NoteText = note.Text;

        _mapper = mapper;
        _noteService = noteService;

        _refreshNotes = refreshNotes;
        _cancel = cancel;
    }

    [RelayCommand]
    private async Task Save()
    {
        if (_noteId == null)
        {
            await _noteService.AddNoteAsync(_plantId, NoteText);
            await _refreshNotes();
        }
        else
        {
            await _noteService.UpdataNoteAsync(_noteId.Value, NoteText);
            await _refreshNotes();
        }

    }

    [RelayCommand]
    private void Cancel()
    {
        _cancel();
    }
}

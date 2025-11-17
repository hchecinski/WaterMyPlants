using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Factories;

public interface INoteFactory
{
    NoteFormViewModel CreateNoteFormViewModel(Guid plantId, Func<Task> refreshNotes, Action cancel);
    NoteFormViewModel CreateNoteFormViewModel(NoteModel note, Func<Task> refreshNotes, Action cancel);
}

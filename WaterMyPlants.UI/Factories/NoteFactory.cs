using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Factories;

public class NoteFactory : INoteFactory
{
    public IServiceProvider _serviceProvider { get; }

    public NoteFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;    
    }

    public NoteFormViewModel CreateNoteFormViewModel(Guid plantId, Func<Task> refreshNotes, Action cancel)
    {
        return ActivatorUtilities.CreateInstance<NoteFormViewModel>(_serviceProvider, plantId, refreshNotes, cancel);
    }

    public NoteFormViewModel CreateNoteFormViewModel(NoteModel note, Func<Task> refreshNotes, Action cancel)
    {
        return ActivatorUtilities.CreateInstance<NoteFormViewModel>(_serviceProvider, note, refreshNotes, cancel);
    }
}

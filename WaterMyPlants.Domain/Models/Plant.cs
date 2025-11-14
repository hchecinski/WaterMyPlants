namespace WaterMyPlants.Domain.Models;

public class Plant : UpdatableEntity
{
    public string Name { get; private set; }
    public string? Localization { get; private set; }
    public string? Description { get; private set; }
    public int WaterIntervalDays { get; private set; }
    public DateTime? LastWaterAt { get; private set; }

    private readonly List<Note> _notes = new();
    public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();

    private readonly List<Photo> _photos = new();
    public IReadOnlyCollection<Photo> Photos => _photos.AsReadOnly();

    public void AddNote(Note note)
    {
        _notes.Add(note);
    }

    public void AddPhoto(Photo photo)
    {
        _photos.Add(photo);
    }

    public void CreateNew()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public void CreateNotes(List<Note> notes)
    {
        _photos.Clear();
        notes.AddRange(notes);  
    }

    public Plant(string name, int waterIntervalDays, string? localization = null, string? description = null)
    {
        Name = name;
        Localization = localization;
        Description = description;
        WaterIntervalDays = waterIntervalDays;
    }

    public Plant() { }
}

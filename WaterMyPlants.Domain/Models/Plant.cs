namespace WaterMyPlants.Domain.Models;

public class Plant : UpdatableEntity
{
    public string Name { get; set; }
    public string? Localization { get; set; }
    public string? Description { get; set; }
    public int WaterIntervalDays { get; set; }
    public DateTime? LastWaterAt { get; set; }

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
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
    }

    public void CreateNotes(IEnumerable<Note> notes)
    {
        _notes.Clear();
        _notes.AddRange(notes);
    }

    public void CreatePhotos(IEnumerable<Photo> photos)
    {
        _photos.Clear();
        _photos.AddRange(photos);
    }

    public void Validate()
    {
        if(string.IsNullOrWhiteSpace(Id))
        {
            throw new ArgumentException("Plant ID cannot be null or empty.");
        }
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new ArgumentException("Plant name cannot be null or empty.");
        }
        if (WaterIntervalDays <= 0)
        {
            throw new ArgumentException("Water interval days must be greater than zero.");
        }
    }

    public void Update(string name, string? description, string? localization, int waterIntervalDays)
    {
        if(!Equals(Name, name))
        {
            Name = name;
        }
        if(!Equals(Description, description))
        {
            Description = description;
        }
        if(!Equals(Localization, localization))
        {
            Localization = localization;
        }
        if(!Equals(WaterIntervalDays, waterIntervalDays))
        {
            WaterIntervalDays = waterIntervalDays;
        }
    }

    public void Water()
    {
        LastWaterAt = DateTime.Now;
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

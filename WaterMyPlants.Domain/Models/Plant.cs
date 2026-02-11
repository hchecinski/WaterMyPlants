using System.Xml.Linq;
using WaterMyPlants.Domain.Exceptions;
using static System.Net.Mime.MediaTypeNames;

namespace WaterMyPlants.Domain.Models;

public sealed class Plant
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Localization { get; private set; }
    public string? Description { get; private set; }
    public int WaterIntervalDays { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastWaterAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }

    private readonly List<Note> _notes = new();
    public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();

    private readonly List<Photo> _photos = new();
    public IReadOnlyCollection<Photo> Photos => _photos.AsReadOnly();

    public void AddNote(Guid noteId, DateTime createAt, string text)
    {
        _notes.Add(Note.Create(noteId, Id, createAt, text));
    }

    public void AddPhoto(Guid photoId, string path, string name, DateTime createAt)
    {
        _photos.Add(Photo.Create(photoId, Id, path, name, createAt));
    }

    public void RemoveNote(Guid noteId)
    {
        Note? note = _notes.FirstOrDefault(n => n.Id == noteId);
        if (note is null)
        {
            throw new DomainRuleViolationException($"Note with id {noteId} not found.");
        }
        _notes.Remove(note);
    }

    public void RemovePhoto(Guid photoId)
    {
        Photo? photo = _photos.FirstOrDefault(p => p.Id == photoId);
        if (photo is null)
        {
            throw new DomainRuleViolationException($"Photo with id {photoId} not found.");
        }
        _photos.Remove(photo);
    }

    public void UpdateNote(Guid noteId, string text, DateTime updatedAt)
    {
        Note? note = _notes.FirstOrDefault(n => n.Id == noteId);
        if (note is null)
        {
            throw new DomainRuleViolationException($"Note with id {noteId} not found.");
        }
        note.Update(text, updatedAt);
    }

    public void UpdatePhoto(Guid photoId, string name, DateTime updatedAt)
    {
        Photo? photo = _photos.FirstOrDefault(p => p.Id == photoId);
        if (photo is null)
        {
            throw new DomainRuleViolationException($"Photo with id {photoId} not found.");
        }
        photo.Update(name, updatedAt);
    }

    private void Validate(string name, int waterInterval)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainRuleViolationException("Plant name cannot be null or empty.");
        }
        if (waterInterval <= 0)
        {
            throw new DomainRuleViolationException("Water interval days must be greater than zero.");
        }
    }

    public void Update(string name, int waterIntervalDays, DateTime updateAt, string? description = null, string? localization = null)
    {
        Validate(name, waterIntervalDays);

        bool isUpdated = false;

        if (!Equals(Name, name))
        {
            Name = name;
            isUpdated = true;
        }
        if (!Equals(Description, description))
        {
            Description = description;
            isUpdated = true;
        }
        if (!Equals(Localization, localization))
        {
            Localization = localization;
            isUpdated = true;
        }
        if (!Equals(WaterIntervalDays, waterIntervalDays))
        {
            WaterIntervalDays = waterIntervalDays;
            isUpdated = true;
        }

        if (isUpdated)
        {
            LastUpdatedAt = updateAt;
        }
    }

    public void Water(DateTime wateringDate)
    {
        LastWaterAt = wateringDate;
    }

    Plant() { }

    public static Plant Create(Guid id, string name, int interval, DateTime createAt, string? localization = null, string? description = null)
    {
        Plant plant = new Plant();

        plant.Validate(name, interval);

        plant.Id = id;
        plant.WaterIntervalDays = interval;
        plant.Name = name;
        plant.CreatedAt = createAt;
        plant.Localization = localization;
        plant.Description = description;

        return plant;
    }
}

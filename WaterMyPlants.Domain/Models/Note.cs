using WaterMyPlants.Domain.Exceptions;

namespace WaterMyPlants.Domain.Models;

public sealed class Note
{
    public Guid Id { get; private set; }
    public Guid PlantId { get; private set; }
    public string Text { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }
    public Plant Plant { get; private set; } = null!;
    private Note() { }

    public static Note Create(Guid noteId, Guid plantId, DateTime createAt, string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new DomainRuleViolationException("Text cannot be empty.");
        }

        if (noteId == Guid.Empty)
        {
            throw new DomainRuleViolationException("NoteId cannot be empty.");
        }

        if (plantId == Guid.Empty)
        {
            throw new DomainRuleViolationException("PlantId cannot be empty.");
        }

        Note note = new Note();
        note.PlantId = plantId;
        note.Id = noteId;
        note.Text = text;
        note.CreatedAt = createAt;
        return note;
    }

    public void Update(string text, DateTime updatedTime)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new DomainRuleViolationException("Text cannot be empty.");
        }

        if (string.Equals(Text, text, StringComparison.Ordinal))
        {
            throw new DomainRuleViolationException("Text cannot be the same as the current text.");
        }

        Text = text;
        LastUpdatedAt = updatedTime;
    }
}

using WaterMyPlants.Domain.Exceptions;

namespace WaterMyPlants.Domain.Models;

public sealed class Note
{
    public Guid Id { get; private set; }
    public string Text { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }

    private Note() { }

    public static Note Create(Guid noteId, DateTime createAt, string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new DomainRuleViolationException("Text cannot be empty.");
        }

        Note note = new Note();
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

        Text = text;
        LastUpdatedAt = updatedTime;
    }
}

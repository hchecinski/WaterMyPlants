namespace WaterMyPlants.Domain.Models;

public class Note : UpdatableEntity
{
    public Guid PlantId { get; private set; }
    public string Text { get; private set; } = string.Empty;

    public Note(Guid id, Guid plantId, string text)
    {
        Id = id;
        PlantId = plantId;
        Text = text;
        CreatedAt = DateTime.UtcNow;
    }
}

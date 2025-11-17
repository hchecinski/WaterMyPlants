namespace WaterMyPlants.Domain.Models;

public class Note : UpdatableEntity
{
    public string PlantId { get; set; }
    public string Text { get; set; } = string.Empty;

    public Note() { }
    public Note(Guid id, DateTime createAt)
    {
         Id = id.ToString();
        CreatedAt = createAt;
    }

    public void Updated(string text)
    {
        Text = text;
        LastUpdatedAt = DateTime.UtcNow;
    }
}

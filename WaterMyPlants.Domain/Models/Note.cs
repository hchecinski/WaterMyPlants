namespace WaterMyPlants.Domain.Models;

public class Note : UpdatableEntity
{
    public string PlantId { get; set; }
    public string Text { get; set; } = string.Empty;

    public Note() { }
}

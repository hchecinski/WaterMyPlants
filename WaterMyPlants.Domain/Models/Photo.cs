namespace WaterMyPlants.Domain.Models;

public class Photo : Entity  
{
    public Guid PlantId { get; private set; }
    public string Path { get; private set; }

    public Photo(Guid id, Guid plantId, string path)
    {
        Id = id;
        PlantId = plantId;
        Path = path;
        CreatedAt = DateTime.UtcNow;
    }

    public Photo() { }
}

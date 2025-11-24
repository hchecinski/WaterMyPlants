
using static System.Net.Mime.MediaTypeNames;

namespace WaterMyPlants.Domain.Models;

public class Photo : UpdatableEntity  
{
    public string PlantId { get; set; }
    public string Path { get; set; }
    public string Name { get; set; }

    public Photo() { }
    public Photo(Guid id, DateTime createAt)
    {
        Id = id.ToString();
        CreatedAt = createAt;
    }

    public void Updated(string newName)
    {
        Name = newName;
        LastUpdatedAt = DateTime.UtcNow;
    }
}

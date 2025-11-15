namespace WaterMyPlants.Domain.Models;

public class Photo : Entity  
{
    public string PlantId { get; set; }
    public string Path { get; set; }

    public Photo() { }
}

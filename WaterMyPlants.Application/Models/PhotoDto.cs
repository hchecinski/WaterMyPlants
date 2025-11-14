namespace WaterMyPlants.Application.Models;

public class PhotoDto
{
    public Guid Id { get; set; }
    public Guid PlantId { get; set; }
    public string Path { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

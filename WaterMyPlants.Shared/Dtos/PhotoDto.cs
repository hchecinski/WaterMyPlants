namespace WaterMyPlants.Shared.Dtos;

public class PhotoDto
{
    public Guid Id { get; set; }
    public Guid PlantId { get; set; }
    public string Path { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Name { get; set; } = string.Empty;
}

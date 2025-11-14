namespace WaterMyPlants.Application.Models;

public class CreatePhotoDto
{
    public Guid PlantId { get; set; }
    public string Path { get; set; } = string.Empty;
}

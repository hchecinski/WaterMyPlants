namespace WaterMyPlants.UI.ViewModels;

public class PhotoViewModel
{
    public Guid Id { get; set; }
    public Guid PlantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Path { get; set; } = string.Empty;
}

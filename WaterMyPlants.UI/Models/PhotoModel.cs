using CommunityToolkit.Mvvm.ComponentModel;

namespace WaterMyPlants.UI.Models;

public partial class PhotoModel : ObservableObject
{
    public Guid Id { get; set; }
    public Guid PlantId { get; set; }
    public string Path { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
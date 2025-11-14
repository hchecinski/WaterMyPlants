using CommunityToolkit.Mvvm.ComponentModel;

namespace WaterMyPlants.UI.Models;

public partial class PlantDetailsModel : ObservableObject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Localization { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastWaterAt { get; set; }
    public DateTime? NextWaterAt { get; set; }

    public IEnumerable<NoteModel> Notes { get; set; } = Enumerable.Empty<NoteModel>();
    public IEnumerable<PhotoModel> Photos { get; set; } = Enumerable.Empty<PhotoModel>();
}
namespace WaterMyPlants.UI.Models;

public partial class PlantListItemModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DaysRemaining { get; set; }
    public string? Localization { get; set; }
    public string? Description { get; set; }
    public string? LastNote { get; set; }
}
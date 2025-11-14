namespace WaterMyPlants.UI.ViewModels;

public class PlantFormViewModel
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Localization { get; set; }
    public string? Description { get; set; }
    public int WaterIntervalDays { get; set; }
}

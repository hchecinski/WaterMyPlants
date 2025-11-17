namespace WaterMyPlants.UI.Models;

public class UpdatablePlantModel
{
    public string Name { get; set; } = string.Empty;
    public string? Localization { get; set; }
    public string? Description { get; set; }
    public int WaterIntervalDays { get; set; }
}

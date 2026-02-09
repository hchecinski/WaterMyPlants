namespace WaterMyPlants.Shared.Models;

public class UpdatablePlantDto
{
    public string Name { get; set; } = string.Empty;
    public string? Localization { get; set; }
    public string? Description { get; set; }
    public int WaterIntervalDays { get; set; }
}

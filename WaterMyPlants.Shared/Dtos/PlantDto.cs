namespace WaterMyPlants.Shared.Dtos;

public class PlantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Localization { get; set; }
    public string? Description { get; set; }
    public int WaterIntervalDays { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? LastWaterAt { get; set; }
}



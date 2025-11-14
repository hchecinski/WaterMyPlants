namespace WaterMyPlants.Application.Models;

public class PlantListItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Localization { get; set; }
    public string? Description { get; set; }
    public string? LastNote { get; set; }
    public int DaysRemaining { get; set; }
}


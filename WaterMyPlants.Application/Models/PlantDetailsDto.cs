namespace WaterMyPlants.Application.Models;

public class PlantDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Localization { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastWaterAt { get; set; }
    public DateTime? NextWaterAt { get; set; }

    public IEnumerable<NoteDto> Notes { get; set; } = new List<NoteDto>();
    public IEnumerable<PhotoDto> Photos { get; set; } = new List<PhotoDto>();
}

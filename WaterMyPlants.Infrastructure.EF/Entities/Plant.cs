using System.ComponentModel.DataAnnotations;

namespace WaterMyPlants.Infrastructure.EF.Entities;

public class Plant
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Localization { get; set; }
    public string? Description { get; set; }

    [Required]
    [Range(1, 365, ErrorMessage = "Water interval must be in a range(1, 365).")]
    public int WaterIntervalDays { get; set; }
    public DateTime? LastWaterAt { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }

    public List<Note> Notes { get; set; } = new();

    public List<Photo> Photos { get; set; } = new();
}

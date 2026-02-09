using System.ComponentModel.DataAnnotations;

namespace WaterMyPlants.Infrastructure.EF.Entities;

public class Photo
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid PlantId { get; set; }

    [Required]
    public string Path { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; protected set; }
}

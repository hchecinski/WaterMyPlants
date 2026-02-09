using System.ComponentModel.DataAnnotations;

namespace WaterMyPlants.Infrastructure.EF.Entities;

public class Note
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid PlantId { get; set; }

    [MaxLength(500)]
    public string? Text { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
}

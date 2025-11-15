using WaterMyPlants.Application.Models;
using WaterMyPlants.Domain.Models;

namespace WaterMyPlants.Application.Services;

public class Mapper : IMapper
{
    public PlantDetailsDto ToPlantDetailsDto(Plant dto)
    {
        if(!Guid.TryParse( dto.Id, out var id))
        {
            throw new InvalidOperationException("Invalid Plant Id format.");
        }

        return new PlantDetailsDto
        {
            Id = id,
            Name = dto.Name,
            Localization = dto.Localization,
            Description = dto.Description,
            CreatedAt = dto.CreatedAt,
            LastWaterAt = dto.LastWaterAt,
            NextWaterAt = dto.LastWaterAt.HasValue
                ? dto.LastWaterAt.Value.AddDays(dto.WaterIntervalDays)
                : null,
            Notes = dto.Notes.Select(n => new NoteDto
            {
                Id = Guid.Parse(n.Id),
                Text = n.Text,
                LastUpdatedAt = n.LastUpdatedAt,
                CreatedAt = n.CreatedAt,
                PlantId = id
            }),
            Photos = dto.Photos.Select(p => new PhotoDto
            {
                Id = Guid.Parse(p.Id),
                Path = p.Path,
                CreatedAt = p.CreatedAt
            })
        };
    }

    public PlantListItemDto ToPlantListItemDto(Plant plant)
    {
        return new PlantListItemDto
        {
            Id = Guid.Parse(plant.Id),
            Name = plant.Name,
            Description = plant.Description,
            Localization = plant.Localization,
            LastNote = plant.Notes.OrderByDescending(n => n.CreatedAt).FirstOrDefault()?.Text,
            DaysRemaining = (plant.LastWaterAt?.AddDays(plant.WaterIntervalDays) - DateTime.UtcNow)?.Days ?? plant.WaterIntervalDays
        };
    }
}

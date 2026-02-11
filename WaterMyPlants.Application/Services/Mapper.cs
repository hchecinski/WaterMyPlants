using WaterMyPlants.Domain.Models;
using WaterMyPlants.Shared.Dtos;

namespace WaterMyPlants.Application.Services;

public class Mapper : IMapper
{
    public NoteDto ToNoteDto(Note note)
    {
        return new NoteDto()
        {
            Id = note.Id,
            PlantId = note.PlantId,
            CreatedAt = note.CreatedAt,
            LastUpdatedAt = note.LastUpdatedAt,
            Text = note.Text
        };
    }

    public PhotoDto ToPhotoListItem(Photo photo)
    {
        return new PhotoDto()
        {
            Id = photo.Id,
            CreatedAt = photo.CreatedAt,
            UpdatedAt = photo.LastUpdatedAt,
            Name = photo.Name,
            Path = photo.Path,
            PlantId = photo.PlantId
        };
    }

    public PlantDetailsDto ToPlantDetailsDto(Plant dto)
    {
        return new PlantDetailsDto
        {
            Id = dto.Id,
            Name = dto.Name,
            Localization = dto.Localization,
            Description = dto.Description,
            CreatedAt = dto.CreatedAt,
            LastWaterAt = dto.LastWaterAt ?? dto.CreatedAt,
            NextWaterAt = dto.LastWaterAt.HasValue
                ? dto.LastWaterAt.Value.AddDays(dto.WaterIntervalDays)
                : dto.CreatedAt.AddDays(dto.WaterIntervalDays),
            Notes = dto.Notes.Select(n => new NoteDto
            {
                Id = n.Id,
                Text = n.Text,
                LastUpdatedAt = n.LastUpdatedAt ?? n.CreatedAt,
                CreatedAt = n.CreatedAt,
                PlantId = n.PlantId
            }),
            Photos = dto.Photos.Select(p => new PhotoDto
            {
                Id = p.Id,
                Path = p.Path,
                CreatedAt = p.CreatedAt
            })
        };
    }

    public PlantListItemDto ToPlantListItemDto(Plant plant)
    {
        return new PlantListItemDto
        {
            Id = plant.Id,
            Name = plant.Name,
            Description = plant.Description,
            Localization = plant.Localization,
            LastNote = plant.Notes.OrderByDescending(n => n.CreatedAt).FirstOrDefault()?.Text,
            DaysRemaining = (plant.LastWaterAt?.AddDays(plant.WaterIntervalDays) - DateTime.UtcNow)?.Days ?? plant.WaterIntervalDays
        };
    }

    public UpdatablePlantDto ToUpdatablePlantDto(Plant plant)
    {
        return new UpdatablePlantDto
        {
            Name = plant.Name,
            Localization = plant.Localization,
            Description = plant.Description,
            WaterIntervalDays = plant.WaterIntervalDays
        };
    }
}

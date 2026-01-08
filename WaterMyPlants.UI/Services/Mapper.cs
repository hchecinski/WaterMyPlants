using WaterMyPlants.Shared.Models;
using WaterMyPlants.UI.Models;

namespace WaterMyPlants.UI.Services;

public class Mapper : IMapper
{

    public PlantListItemModel ToModel(PlantListItemDto dto)
    {
        var viewModel = new PlantListItemModel()
        {
            Id = dto.Id,
            Name = dto.Name,
            DaysRemaining = dto.DaysRemaining,
            Localization = dto.Localization,
            Description = dto.Description,
            LastNote = dto.LastNote
        };
        return viewModel;
    }

    public PlantDetailsModel ToModel(PlantDetailsDto dto)
    {
        var viewModel = new PlantDetailsModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Localization = dto.Localization,
            Description = dto.Description,
            CreatedAt = dto.CreatedAt,
            LastWaterAt = dto.LastWaterAt ?? dto.CreatedAt,
            NextWaterAt = dto.NextWaterAt,

            Notes = dto.Notes.Select(n => new NoteModel
            {
                Id = n.Id,
                Text = n.Text,
                LastSave = n.LastUpdatedAt ?? n.CreatedAt,
                CreatedAt = n.CreatedAt,
                PlantId = n.Id,
            }),
            Photos = dto.Photos.Select(p => new PhotoModel
            {
                Id = p.Id,
                Path = p.Path,
                CreatedAt = p.CreatedAt
            })
        };

        return viewModel;
    }

    public NoteModel ToModel(NoteDto note)
    {
        return new NoteModel
        {
            Id = note.Id,
            PlantId = note.PlantId,
            Text = note.Text,
            CreatedAt = note.CreatedAt,
            LastSave = note.LastUpdatedAt
        };
    }

    public PhotoModel ToModel(PhotoDto photo)
    {
        return new PhotoModel
        {
            Id = photo.Id,
            PlantId = photo.PlantId,
            CreatedAt = photo.CreatedAt,
            Name = photo.Name,
            Path = photo.Path,
            UpdatedAt = photo?.UpdatedAt
        };
    }

    public UpdatePlantDto ToUpdatePlantDto(PlantDetailsModel model)
    {
        return new UpdatePlantDto
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Localization = model.Localization,
            WaterIntervalDays = model.NextWaterAt.HasValue && model.LastWaterAt.HasValue
                ? (model.NextWaterAt.Value - model.LastWaterAt.Value).Days
                : 0
        };
    }
}

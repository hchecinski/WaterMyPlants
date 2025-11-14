using WaterMyPlants.Application.Models;
using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.ViewModels;

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
            LastWaterAt = dto.LastWaterAt,
            NextWaterAt = dto.NextWaterAt,

            Notes = dto.Notes.Select(n => new NoteModel
            {
                Id = n.Id,
                Text = n.Text,
                LastSave = n.LastSave
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
}

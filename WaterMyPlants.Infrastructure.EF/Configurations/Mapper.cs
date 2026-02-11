
namespace WaterMyPlants.Infrastructure.EF.Configurations;

public class Mapper : IMapper
{
    public Entities.Note ToNoteEntity(Domain.Models.Note note)
    {
        return new Entities.Note
        {
            Id = note.Id,
            PlantId = note.PlantId,
            CreatedAt = note.CreatedAt,
            LastUpdatedAt = note.LastUpdatedAt,
            Text = note.Text
        };
    }

    public Domain.Models.Note ToNoteModel(Entities.Note note)
    {
        throw new NotImplementedException();
    }

    public Entities.Photo ToPhotoEntity(Domain.Models.Photo photo)
    {
        throw new NotImplementedException();
    }

    public Domain.Models.Photo ToPhotoModel(Entities.Photo photo)
    {
        throw new NotImplementedException();
    }

    public Entities.Plant ToPlantEntity(Domain.Models.Plant plant)
    {
        return  new Entities.Plant
        {
            Id = plant.Id,
            Name = plant.Name,
            Description = plant.Description,
            Localization = plant.Localization,
            WaterIntervalDays = plant.WaterIntervalDays,
            LastWaterAt = plant.LastWaterAt,
            CreatedAt = plant.CreatedAt,
            LastUpdatedAt = plant.LastUpdatedAt
        };
    }

    public Domain.Models.Plant ToPlantModel(Entities.Plant plant)
    {
        var model = Domain.Models.Plant.CreateNew(
            plant.Id,
            plant.Name,
            plant.WaterIntervalDays,
            plant.Localization,
            plant.Description
        );

        model.ReplaceNotes(plant.Notes.Select(ToNoteModel));
        model.ReplacePhotos(plant.Photos.Select(ToPhotoModel));

        return model;
    }
}

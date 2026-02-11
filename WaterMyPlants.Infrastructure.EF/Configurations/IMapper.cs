namespace WaterMyPlants.Infrastructure.EF.Configurations;

public interface IMapper
{
    Entities.Note ToNoteEntity(Domain.Models.Note note);
    Entities.Photo ToPhotoEntity(Domain.Models.Photo photo);
    Entities.Plant ToPlantEntity(Domain.Models.Plant plant);
    Domain.Models.Note ToNoteModel(Entities.Note note);
    Domain.Models.Photo ToPhotoModel(Entities.Photo photo);
    Domain.Models.Plant ToPlantModel(Entities.Plant plant);
}

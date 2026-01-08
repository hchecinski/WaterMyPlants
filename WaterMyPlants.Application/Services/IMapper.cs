using WaterMyPlants.Domain.Models;
using WaterMyPlants.Shared.Models;

namespace WaterMyPlants.Application.Services;

public interface IMapper
{
    NoteDto ToNoteDto(Note note);
    PhotoDto ToPhotoListItem(Photo photo);
    PlantDetailsDto ToPlantDetailsDto(Plant dto);
    PlantListItemDto ToPlantListItemDto(Plant dto);
    UpdatablePlantDto ToUpdatablePlantDto(Plant plant);
}

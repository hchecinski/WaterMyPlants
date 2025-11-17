using WaterMyPlants.Application.Models;
using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Services;

public interface IMapper
{
    PlantListItemModel ToModel(PlantListItemDto dto);
    PlantDetailsModel ToModel(PlantDetailsDto dto);
    NoteModel ToModel(NoteDto note);
    UpdatePlantDto ToUpdatePlantDto(PlantDetailsModel model);
}

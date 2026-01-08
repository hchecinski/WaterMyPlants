using WaterMyPlants.Shared.Models;
using WaterMyPlants.UI.Models;

namespace WaterMyPlants.UI.Services;

public interface INavigationService
{
    Task GoBack();
    Task BackToPlantDetailsPage(PlantDetailsModel plant);
    Task NavigateToPlantDetailsPage(PlantDetailsModel plant);
    Task NavigateToPlantFormPage(UpdatePlantDto? plant = null);
}
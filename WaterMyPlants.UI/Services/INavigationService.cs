
using WaterMyPlants.Application.Models;
using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Services;

public interface INavigationService
{
    Task GoBack();
    Task BackToPlantDetailsPage(PlantDetailsModel plant);
    Task NavigateToPlantDetailsPage(PlantDetailsModel plant);
    Task NavigateToPlantFormPage(UpdatePlantDto? plant = null);
}
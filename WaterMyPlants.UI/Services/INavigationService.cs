
using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Services;

public interface INavigationService
{
    Task NavigateToPlantDetailsPage(PlantDetailsModel model);
}
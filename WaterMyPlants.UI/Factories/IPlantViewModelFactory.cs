using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Factories;

public interface IPlantViewModelFactory
{
    PlantListItemViewModel Create(PlantListItemModel model);
    PlantDetailsViewModel Create(PlantDetailsModel model);
}
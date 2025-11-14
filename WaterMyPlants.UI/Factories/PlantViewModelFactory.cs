using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Factories;

public class PlantViewModelFactory : IPlantViewModelFactory
{
    private readonly IServiceProvider _provider;

    public PlantViewModelFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public PlantListItemViewModel Create(PlantListItemModel model)
    {
        return ActivatorUtilities.CreateInstance<PlantListItemViewModel>(_provider, model);
    }

    public PlantDetailsViewModel Create(PlantDetailsModel model)
    {
        return ActivatorUtilities.CreateInstance<PlantDetailsViewModel>(_provider, model);
    }
}
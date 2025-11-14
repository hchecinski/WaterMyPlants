using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.Views;

namespace WaterMyPlants.UI.Services;

internal class NavigationService : INavigationService
{
    public async Task NavigateToPlantDetailsPage(PlantDetailsModel plant)
    {
        await Shell.Current.GoToAsync(nameof(PlantDetailsPage), new Dictionary<string, object>
        {
            { "Model" , plant }
        });
    }
}

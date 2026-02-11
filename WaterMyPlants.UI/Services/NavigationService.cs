using WaterMyPlants.Shared.Dtos;
using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.Views;

namespace WaterMyPlants.UI.Services;

public class NavigationService : INavigationService
{
    public async Task BackToPlantDetailsPage(PlantDetailsModel plant)
    {
        await Shell.Current.GoToAsync("..", new Dictionary<string, object>
        {
            { "Model" , plant }
        });
    }

    public async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    public async Task NavigateToPlantDetailsPage(PlantDetailsModel plant)
    {
        await Shell.Current.GoToAsync(nameof(PlantDetailsPage), new Dictionary<string, object>
        {
            { "Model" , plant }
        });
    }

    public async Task NavigateToPlantFormPage(UpdatePlantDto? plant)
    {
        if (plant == null)
        {
            await Shell.Current.GoToAsync(nameof(PlantFormPage));
            return;
        }

        await Shell.Current.GoToAsync(nameof(PlantFormPage), new Dictionary<string, object>()
        {
            { "Model", plant }
        });
    }
}

using CommunityToolkit.Mvvm.Input;

namespace WaterMyPlants.UI.ViewModels;

public partial class MainViewModel
{
    [RelayCommand]
    private async Task AddPlant()
    {
        await Task.CompletedTask;
    }
}

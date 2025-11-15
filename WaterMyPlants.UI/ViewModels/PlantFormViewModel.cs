using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WaterMyPlants.Application.Models;
using WaterMyPlants.Application.Services;
using WaterMyPlants.UI.Services;

namespace WaterMyPlants.UI.ViewModels;

public partial class PlantFormViewModel : ObservableObject
{
    private readonly IPlantService _plantService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private Guid? _id;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string? _localization;

    [ObservableProperty]
    private string? _description;

    [ObservableProperty]
    private int _waterIntervalDays;

    [RelayCommand]
    private async Task Add()
    {
        await _plantService.AddAsync(new CreatePlantDto()
        {
            Name = Name,
            Localization = Localization,
            Description = Description,
            WaterIntervalDays = WaterIntervalDays
        });

        await _navigationService.GoBack();
    }

    public PlantFormViewModel(IPlantService plantService, INavigationService navigationService)
    {
        _plantService = plantService;
        _navigationService = navigationService; 
    }
}

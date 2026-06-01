using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.Services;

namespace WaterMyPlants.UI.ViewModels;

[QueryProperty(nameof(Model), "Model")]
public partial class PlantFormViewModel : ObservableObject
{
    private readonly IPlantService _plantService;
    private readonly INavigationService _navigationService;
    private readonly string _newPlantTitle = "Nowa roślinka";
    private readonly string _updatePlantTitle = "Edytowana roślinka";

    [ObservableProperty]
    private PlantDetailsModel? _model;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string? _localization;

    [ObservableProperty]
    private string? _description;

    [ObservableProperty]
    private int _waterIntervalDays;

    [ObservableProperty]
    private string _title;

    [RelayCommand]
    private async Task Save()
    {
        Guid id = Model?.Id ?? Guid.Empty;

        var plant = new UpdatePlantModel()
        {
            Name = Name,
            Localization = Localization,
            Description = Description,
            WaterIntervalDays = WaterIntervalDays
        };


        if(id == Guid.Empty)
        {
            await _plantService.AddAsync(plant);
            await _navigationService.GoBack();
        }
        else
        {
            await _plantService.UpdateAsync(plant);
            var plantDetails = await _plantService.GetDetailsAsync(id);

            if(plantDetails == null)
            {
                //Zapisz do loga.
                return;    
            }

            await _navigationService.BackToPlantDetailsPage(plantDetails);
        }

    }

    public PlantFormViewModel(IPlantService plantService, INavigationService navigationService)
    {
        _title = _newPlantTitle;

        _plantService = plantService;
        _navigationService = navigationService; 
    }

    private async Task<UpdatePlantModel?> GetPlantAsync()
    {
        if (Model is null)
        {
            return null;
        }

        return await _plantService.GetUpdatablePlantAsync(Model.Id);
    }

    public void SetEditMode(string name, string? localization, string? description, int waterIntervalDays)
    {
        Name = name;
        Localization = localization;
        Description = description;
        WaterIntervalDays = waterIntervalDays;
        Title = _updatePlantTitle;
    }

    internal void OnAppearing()
    {
        if (Model is null)
        {
            return;
        }

        SetEditMode(Model.Name, Model.Localization, Model.Description, Model.WaterIntervalDays);
    }
}

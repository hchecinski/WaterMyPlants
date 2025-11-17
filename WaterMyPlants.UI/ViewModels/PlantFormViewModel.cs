using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WaterMyPlants.Application.Models;
using WaterMyPlants.Application.Services;
using WaterMyPlants.UI.Services;

namespace WaterMyPlants.UI.ViewModels;

[QueryProperty(nameof(Model), "Model")]
public partial class PlantFormViewModel : ObservableObject
{
    private readonly IPlantService _plantService;
    private readonly INavigationService _navigationService;
    private readonly IMapper _mapper;
    private readonly string _newPlantTitle = "Nowa roślinka";
    private readonly string _updatePlantTitle = "Edytowana roślinka";

    [ObservableProperty]
    private UpdatePlantDto? _model;

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

        var plant = new UpdatePlantDto()
        {
            Id = id,
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
            var plantDetailsDto = await _plantService.GetDetailsAsync(id);
            var plantDetailsModel = _mapper.ToModel(plantDetailsDto);
            await _navigationService.BackToPlantDetailsPage(plantDetailsModel);
        }

    }

    public PlantFormViewModel(IPlantService plantService, INavigationService navigationService, IMapper mapper)
    {
        _title = _newPlantTitle;

        _plantService = plantService;
        _navigationService = navigationService; 
        _mapper = mapper;
    }

    private async Task<UpdatablePlantDto?> GetPlantAsync()
    {
        if (Model == null)
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
        if (Model == null)
        {
            return;
        }

        SetEditMode(Model.Name, Model.Localization, Model.Description, Model.WaterIntervalDays);
    }
}

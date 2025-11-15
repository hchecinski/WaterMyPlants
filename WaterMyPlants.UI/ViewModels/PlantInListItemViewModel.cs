using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using WaterMyPlants.Application.Services;
using WaterMyPlants.UI.Factories;
using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.Services;
using IMapper = WaterMyPlants.UI.Services.IMapper;

namespace WaterMyPlants.UI.ViewModels;

public partial class PlantListItemViewModel : ObservableObject
{
    public PlantListItemModel Model { get; }

    public string Name => Model.Name;
    public string? Localization => Model.Localization;
    public int DaysRemaining => Model.DaysRemaining;
    public string? Description => Model.Description;
    public string? LastNote => Model.LastNote;

    [ObservableProperty]
    private bool _isSelected;

    private readonly INavigationService _navigationService;
    private readonly IMapper _mapper;
    private readonly IPlantService _plantService;
    private readonly IPlantViewModelFactory _factory;

    public PlantListItemViewModel(PlantListItemModel model, INavigationService navigationService, IMapper mapper, IPlantService plantService, IPlantViewModelFactory plantViewModelFactory)
    {
        Model = model;

        _navigationService = navigationService;
        _mapper = mapper;
        _plantService = plantService;
        _factory = plantViewModelFactory;
    }


    private async Task<PlantDetailsModel?> GetDetailsAsync()
    {
        try
        {
            var dto = await _plantService.GetDetailsAsync(Model.Id);
            return _mapper.ToModel(dto);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR loading plant details: {ex.Message}");
            return null;
        }
    }

    [RelayCommand]
    private async Task MoreInfo()
    {
        var plantDetails = await GetDetailsAsync();

        if (plantDetails == null)
        {
            Debug.WriteLine("--run: PlantListItemViewModel.MoreInfo() => 'plantDetails is null'");
            return;
        }

        await _navigationService.NavigateToPlantDetailsPage(plantDetails);
    }

    [RelayCommand]
    private void SelectItem()
    {
        IsSelected = !IsSelected;
    }
}

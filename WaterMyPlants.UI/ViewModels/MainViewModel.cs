using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WaterMyPlants.UI.Factories;
using WaterMyPlants.UI.Services;

namespace WaterMyPlants.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IPlantService _plantService;
    private readonly IMapper _mapper;
    private readonly IPlantViewModelFactory _factory;
    private readonly INavigationService _navigator;

    private CancellationTokenSource _wateringToken;

    [ObservableProperty]
    ObservableCollection<PlantListItemViewModel> _plants = new();

    public MainViewModel(IPlantService plantService, IMapper mapper, IPlantViewModelFactory plantViewModelFactory, INavigationService navigationService)
    {
        _plantService = plantService;
        _mapper = mapper;
        _factory = plantViewModelFactory;
        _navigator = navigationService;

        _wateringToken = new CancellationTokenSource();
    }

    public async Task OnAppearing()
    {
        var plants = await GetPlantsAsync();
        UpdatePlants(plants);
    }

    private async Task<IEnumerable<PlantListItemViewModel>> GetPlantsAsync()
    {
        try
        {
            var dtos = await _plantService.GetSortedAsync();
            return dtos.Select(_mapper.ToModel).Select(_factory.Create);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR loading plants: {ex.Message}");
            return Enumerable.Empty<PlantListItemViewModel>();
        }
    }

    private void UpdatePlants(IEnumerable<PlantListItemViewModel>? list)
    {
        App.Current?.Dispatcher.Dispatch(() =>
        {
            Plants.Clear();
            if (list == null || !list.Any())
            {
                return;
            }

            foreach (var item in list)
            {
                Plants.Add(item);
            }
        });
    }

    [RelayCommand]
    private async Task AddPlant()
    {
        await _navigator.NavigateToPlantFormPage();
    }

    [RelayCommand]
    private async Task WaterPlant(PlantListItemViewModel model)
    {
        var result = await Shell.Current.DisplayAlert("Podlewanie", $"Czy na pewno chcesz podlać roślinkę {model.Name}?", "Tak", "Anuluj");
        if (!result)
        {
            return;
        }
        await _plantService.WaterNowAsync(model.Model.Id);
        await OnAppearing();
    }

    [RelayCommand]
    private  async Task CancelWateringPlan()
    {
        await _wateringToken.CancelAsync();
        _wateringToken = new CancellationTokenSource();
    }
}

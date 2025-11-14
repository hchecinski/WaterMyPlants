using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WaterMyPlants.Application.Services;
using WaterMyPlants.UI.Factories;
using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.Services;

namespace WaterMyPlants.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IPlantService _plantService;
    private readonly IMapper _mapper;
    private readonly IPlantViewModelFactory _factory;

    [ObservableProperty]
    ObservableCollection<PlantListItemViewModel> _plants = new();

    public MainViewModel(IPlantService plantService, IMapper mapper, IPlantViewModelFactory plantViewModelFactory)
    {
        _plantService = plantService;
        _mapper = mapper;
        _factory = plantViewModelFactory;
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
}

using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using WaterMyPlants.Application.Services;
using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.Services;

namespace WaterMyPlants.UI.ViewModels;

[QueryProperty(nameof(Model), "Model")]
public partial class PlantDetailsViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IPlantService _plantService;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private PlantDetailsModel _model;

    [ObservableProperty]
    private string _noteText;
    [ObservableProperty]
    private bool _isVisibleNoteEditor;
    [ObservableProperty]
    private bool _actionBtnIsVisible = true;
    [ObservableProperty]
    private string _name = string.Empty;
    [ObservableProperty]
    private string? _localization = string.Empty;
    [ObservableProperty]
    private string? _description  = string.Empty;
    [ObservableProperty]
    private DateTime? _createdAt;
    [ObservableProperty]
    private DateTime? _lastWaterAt;
    [ObservableProperty]
    private DateTime? _nextWaterAt;
    [ObservableProperty]
    private ObservableCollection<NoteModel> _notes = new();
    [ObservableProperty]
    private ObservableCollection<PhotoModel> _photos = new();

    partial void OnModelChanged(PlantDetailsModel value)
    {
        if (value == null)
            return;

        Name = value.Name;
        Localization = value.Localization;
        Description = value.Description;
        CreatedAt = value.CreatedAt;
        LastWaterAt = value.LastWaterAt;
        NextWaterAt = value.NextWaterAt;
        Notes = value.Notes?.ToObservableCollection() ?? new ObservableCollection<NoteModel>();
        Photos = value.Photos?.ToObservableCollection() ?? new ObservableCollection<PhotoModel>();
    }

    public PlantDetailsViewModel(INavigationService navigationService, IPlantService plantService, IMapper mapper)
    {
        _navigationService = navigationService;
        _plantService = plantService;
        _mapper = mapper;
    }

    private async Task WaterAsync()
    {
        await Shell.Current.DisplayAlert("Podlej roślinę", "Funkcjonalność w budowie.", "OK");
    }

    private async Task AddNoteAsync()
    {
        ActionBtnIsVisible = false;
        IsVisibleNoteEditor = true;
        NoteText = string.Empty;
    }

    private async Task AddPhotoAsync()
    {
        await Shell.Current.DisplayAlert("Dodaj zdjęcie", "Funkcjonalność w budowie.", "OK");
    }

    private async Task EditAsync()
    {
        var plant = _mapper.ToUpdatePlantDto(Model);
        await _navigationService.NavigateToPlantFormPage(plant);
    }

    private async Task DeleteAsync()
    {
        var question = $"Czy napewno chcesz usunąć {Name}? Wszystkie dane zostaną bezpowrotnie skasowane.";
        var delete = await Shell.Current.DisplayAlert("Ostrzeżenie", question, accept: "Tak", cancel: "Nie");

        if(!delete)
        {
            return;
        }

        await _plantService.DeleteAsync(Model.Id);
        await _navigationService.GoBack();
    }

    [RelayCommand]
    public async Task ShowActions()
    {
        string action = await Shell.Current.DisplayActionSheet(
            "Akcje",
            "Cofnij",
            null,
            "Podlej roślinę",
            "Dodaj notatkę",
            "Dodaj zdjęcie",
            "Edytuj roślinę",
            "Usuń roślinę"
        );

        switch (action)
        {
            case "Podlej roślinę":
                await WaterAsync();
                break;
            case "Dodaj notatkę":
                await AddNoteAsync();
                break;
            case "Dodaj zdjęcie":
                await AddPhotoAsync();
                break;
            case "Edytuj roślinę":
                await EditAsync();
                break;
            case "Usuń roślinę":
                await DeleteAsync();
                break;
        }
    }

    [RelayCommand]
    private async Task SaveNote()
    {
        if (string.IsNullOrWhiteSpace(NoteText))
        {
            await Shell.Current.DisplayAlert("Błąd", "Notatka nie może być pusta.", "OK");
            return;
        }
        IsVisibleNoteEditor = false;
        ActionBtnIsVisible = true;
    }
}

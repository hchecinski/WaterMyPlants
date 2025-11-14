
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WaterMyPlants.UI.Models;

namespace WaterMyPlants.UI.ViewModels;

[QueryProperty(nameof(Model), "Model")]
public partial class PlantDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private PlantDetailsModel _model;

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

    private async Task WaterAsync()
    {
        // TODO
    }

    private async Task AddNoteAsync()
    {
        // TODO
    }

    private async Task AddPhotoAsync()
    {
        // TODO
    }

    private async Task EditAsync()
    {
        // TODO
    }

    private async Task DeleteAsync()
    {
        // TODO
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
}

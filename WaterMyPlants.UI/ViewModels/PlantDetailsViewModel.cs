using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WaterMyPlants.Application.Models;
using WaterMyPlants.Application.Services;
using WaterMyPlants.UI.Factories;
using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.Services;

namespace WaterMyPlants.UI.ViewModels;

[QueryProperty(nameof(Model), "Model")]
public partial class PlantDetailsViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IPlantService _plantService;
    private readonly INoteService _noteService;
    private readonly IMapper _mapper;
    private readonly INoteFactory _noteFactory;
    private readonly IPhotoFactory _photoFactory;
    private readonly IPhotoService _photoService;

    [ObservableProperty]
    private PlantDetailsModel _model;

    [ObservableProperty]
    private NoteFormViewModel _noteEditorViewModel;

    [ObservableProperty]
    private PhotoFormViewModel _photoFormViewModel;

    [ObservableProperty]
    private bool _isVisiblePhotoForm = false;

    [ObservableProperty]
    private bool _isVisibleNoteEditor = false;

    [ObservableProperty]
    private bool _actionBtnIsVisible = true;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string? _localization = string.Empty;

    [ObservableProperty]
    private string? _description = string.Empty;

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

    public PlantDetailsViewModel(INavigationService navigationService, IPlantService plantService, INoteService noteService, IPhotoService photoService, IMapper mapper, INoteFactory noteFactory, IPhotoFactory photoFactory)
    {
        _navigationService = navigationService;
        _plantService = plantService;
        _noteService = noteService;
        _mapper = mapper;
        _noteFactory = noteFactory;
        _photoFactory = photoFactory;
        _photoService = photoService;
    }

    private async Task WaterAsync()
    {
        await Shell.Current.DisplayAlert("Podlej roślinę", "Funkcjonalność w budowie.", "OK");
    }

    private void AddNote()
    {
        ActionBtnIsVisible = false;
        IsVisiblePhotoForm = false;
        IsVisibleNoteEditor = true;


        NoteEditorViewModel = _noteFactory.CreateNoteFormViewModel(Model.Id, RefreshNotes, CancelEditors);
    }

    private async Task AddPhotoAsync()
    {
        var photoStream = await TakePhotoAsync();
        if (photoStream == null)
        {
            return;
        }

        ActionBtnIsVisible = false;
        IsVisiblePhotoForm = true;
        IsVisibleNoteEditor = false;

        PhotoFormViewModel = _photoFactory.CreatePhotoFormViewModel(Model.Id, photoStream!, RefreshPhotos, CancelEditors);
    }

    private async Task<Stream?> TakePhotoAsync()
    {

        if (!MediaPicker.Default.IsCaptureSupported)
        {
            return null;
        }
        FileResult? result = null;

        try
        {
            result = await MediaPicker.Default.CapturePhotoAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return null;
        }

        var sourceStream = await result!.OpenReadAsync();

        return sourceStream;
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

        if (!delete)
        {
            return;
        }

        await _plantService.DeleteAsync(Model.Id);
        await _navigationService.GoBack();
    }

    private async Task RefreshNotes()
    {
        var notes = await _noteService.GetNotes(Model.Id);
        Notes.Clear();
        foreach (var note in notes)
        {
            var noteModel = _mapper.ToModel(note);
            Notes.Add(noteModel);
        }

        ActionBtnIsVisible = true;
        IsVisibleNoteEditor = false;
        IsVisiblePhotoForm = false;
    }

    private async Task RefreshPhotos()
    {
        IEnumerable<PhotoDto> photos = await _photoService.GetPhotos(Model.Id);
        Photos.Clear();
        foreach (var photo in photos)
        {
            var photoModel = _mapper.ToModel(photo);
            Photos.Add(photoModel);
        }

        ActionBtnIsVisible = true;
        IsVisibleNoteEditor = false;
        IsVisiblePhotoForm = false;
    }

    private void CancelEditors()
    {
        ActionBtnIsVisible = true;
        IsVisibleNoteEditor = false;
        IsVisiblePhotoForm = false;
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
                AddNote();
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
    private async Task DeleteNote(NoteModel noteModel)
    {
        var question = $"Czy napewno chcesz usunąć notatkę?";
        var delete = await Shell.Current.DisplayAlert("Ostrzeżenie", question, accept: "Tak", cancel: "Nie");

        if (!delete)
        {
            return;
        }

        await _noteService.DeleteNoteAsync(noteModel.Id);
        await RefreshNotes();
    }

    [RelayCommand]
    private void EditNote(NoteModel noteModel)
    {
        ActionBtnIsVisible = false;
        IsVisiblePhotoForm = false;
        IsVisibleNoteEditor = true;

        NoteEditorViewModel = _noteFactory.CreateNoteFormViewModel(noteModel, RefreshNotes, CancelEditors);
    }

    [RelayCommand]
    private async Task DeletePhoto(PhotoModel photoModel)
    {
        var question = $"Czy napewno chcesz usunąć zdjęcie?";
        var delete = await Shell.Current.DisplayAlert("Ostrzeżenie", question, accept: "Tak", cancel: "Nie");

        if (!delete)
        {
            return;
        }

        await _photoService.DeletePhotoAsync(photoModel.Id);
        await RefreshPhotos();
    }

    [RelayCommand]
    private void EditPhoto(PhotoModel photoModel)
    {
        ActionBtnIsVisible = false;
        IsVisiblePhotoForm = true;
        IsVisibleNoteEditor = false;

        PhotoFormViewModel = _photoFactory.CreatePhotoFormViewModel(photoModel, RefreshPhotos, CancelEditors);
    }
}

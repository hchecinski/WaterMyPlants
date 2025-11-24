using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WaterMyPlants.Application.Services;
using WaterMyPlants.UI.Models;

namespace WaterMyPlants.UI.ViewModels;

public partial class PhotoFormViewModel : ObservableObject, IDisposable
{
    private readonly IPhotoService _photoService;

    private readonly Guid? _photoId;
    private readonly Guid _plantId;
    private readonly Func<Task> _refreshPhotos;
    private readonly Action _cancel;
    private readonly MemoryStream _stream = new();

    [ObservableProperty]
    private string _photoName = string.Empty;

    [ObservableProperty]
    private ImageSource _photo = string.Empty;

    public PhotoFormViewModel(Guid plantId, Stream photoStream, Func<Task> refreshPhotos, Action cancel, IPhotoService photoService)
    {
        _photoService = photoService;
        _plantId = plantId;
        _refreshPhotos = refreshPhotos;
        _cancel = cancel;

        photoStream.CopyTo(_stream);
        Photo = ImageSource.FromStream(() => new MemoryStream(_stream.ToArray()));
    }

    public PhotoFormViewModel(PhotoModel photo, Func<Task> refreshPhotos, Action cancel, IPhotoService photoService)
    {
        _photoId = photo.Id;
        _photoName = photo.Name;

        _photoService = photoService;

        _refreshPhotos = refreshPhotos;
        _cancel = cancel;
        var stream = File.OpenRead(photo.Path);
        Photo = ImageSource.FromStream(() => stream);
    }

    [RelayCommand]
    private async Task Save()
    {
        if (_photoId == null)
        {
            if(string.IsNullOrEmpty(PhotoName?.Trim()))
            {
                return;
            }

            var fileName = $"{Guid.NewGuid()}.jpg";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            try
            {
                using var newStream = File.OpenWrite(filePath);
                _stream.Seek(0, SeekOrigin.Begin);
                await _stream.CopyToAsync(newStream);
            }
            catch (Exception ex)
            {
                return;
            }

            try
            {
                await _photoService.AddPhotoAsync(_plantId, PhotoName, filePath);
            }
            catch (Exception ex)
            {
                File.Delete(filePath);
                return;
            }

            await _refreshPhotos();
        }
        else
        {
            await _photoService.UpdataPhotoAsync(_photoId.Value, PhotoName);
            await _refreshPhotos();
        }

    }

    [RelayCommand]
    private void Cancel()
    {
        _cancel();
    }

    public void Dispose()
    {
        ((IDisposable)_stream)?.Dispose();
        _stream?.Close();
    }
}

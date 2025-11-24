using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Factories;

public class PhotoFactory : IPhotoFactory
{
    public IServiceProvider _serviceProvider { get; }

    public PhotoFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public PhotoFormViewModel CreatePhotoFormViewModel(Guid plantId, Stream photoStream, Func<Task> refreshPhotos, Action cancel)
    {
        return ActivatorUtilities.CreateInstance<PhotoFormViewModel>(_serviceProvider, plantId, photoStream, refreshPhotos, cancel);
    }

    public PhotoFormViewModel CreatePhotoFormViewModel(PhotoModel photo, Func<Task> refreshPhotos, Action cancel)
    {
        return ActivatorUtilities.CreateInstance<PhotoFormViewModel>(_serviceProvider, photo, refreshPhotos, cancel);
    }
}

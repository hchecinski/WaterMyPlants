using WaterMyPlants.UI.Models;
using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Factories;

public interface IPhotoFactory
{
    PhotoFormViewModel CreatePhotoFormViewModel(Guid plantId, Stream photoStream, Func<Task> refreshPhotos, Action cancel);
    PhotoFormViewModel CreatePhotoFormViewModel(PhotoModel photo, Func<Task> refreshPhotos, Action cancel);
}

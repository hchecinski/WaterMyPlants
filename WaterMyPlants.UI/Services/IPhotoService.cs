using WaterMyPlants.Shared.Models;

namespace WaterMyPlants.UI.Services;

public interface IPhotoService
{
    Task AddPhotoAsync(Guid plantId, string photoName, string path);
    Task<PhotoDto?> GetPhotoAsync(Guid photoId);
    Task<IEnumerable<PhotoDto>> GetPhotos(Guid plantId);
    Task UpdataPhotoAsync(Guid photoId, string photoName);
    Task DeletePhotoAsync(Guid photoId);
}

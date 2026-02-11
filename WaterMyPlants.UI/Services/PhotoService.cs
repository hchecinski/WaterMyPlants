using WaterMyPlants.Shared.Dtos;

namespace WaterMyPlants.UI.Services;

public class PhotoService : IPhotoService
{
    public Task AddPhotoAsync(Guid plantId, string photoName, string path)
    {
        throw new NotImplementedException();
    }

    public Task DeletePhotoAsync(Guid photoId)
    {
        throw new NotImplementedException();
    }

    public Task<PhotoDto?> GetPhotoAsync(Guid photoId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PhotoDto>> GetPhotos(Guid plantId)
    {
        throw new NotImplementedException();
    }

    public Task UpdataPhotoAsync(Guid photoId, string photoName)
    {
        throw new NotImplementedException();
    }
}

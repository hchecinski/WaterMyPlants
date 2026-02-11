using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;

namespace WaterMyPlants.Infrastructure.EF.Repositories;

public class PhotoRepository : IPhotoRepository
{
    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Photo>> GetAllAsync(Guid plantId)
    {
        throw new NotImplementedException();
    }

    public Task<Photo?> GetAsync(Guid photoId)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(Photo entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Photo entity)
    {
        throw new NotImplementedException();
    }
}

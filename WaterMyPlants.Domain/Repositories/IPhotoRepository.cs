using System.Collections.Generic;
using WaterMyPlants.Domain.Models;

namespace WaterMyPlants.Domain.Repositories;

public interface IPhotoRepository : IUpdatableRepository<Photo>
{
    Task<IEnumerable<Photo>> GetAllAsync(Guid plantId);
    Task<Photo?> GetAsync(Guid photoId);
}
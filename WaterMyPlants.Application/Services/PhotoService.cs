using System.Net.NetworkInformation;
using WaterMyPlants.Application.Models;
using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace WaterMyPlants.Application.Services;

public class PhotoService : IPhotoService
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IMapper _mapper;

    public PhotoService(IPhotoRepository photoRepository, IMapper mapper)
    {
        _photoRepository = photoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PhotoDto>> GetPhotos(Guid plantId)
    {
        var photos = await _photoRepository.GetAllAsync(plantId);

        if (!photos.Any())
        {
            return Enumerable.Empty<PhotoDto>();
        }

        return photos.Select(_mapper.ToPhotoListItem);
    }

    public async Task<PhotoDto?> GetPhotoAsync(Guid photoId)
    {
        var photo = await _photoRepository.GetAsync(photoId);

        if (photo == null)
        {
            return null;
        }

        return _mapper.ToPhotoListItem(photo);
    }

    public async Task AddPhotoAsync(Guid plantId, string photoName, string path)
    {
        Photo photo = new Photo(Guid.NewGuid(), DateTime.UtcNow)
        {
            PlantId = plantId.ToString(),
            Name = photoName,
            Path = path
        };

        await _photoRepository.InsertAsync(photo);
    }

    public async Task UpdataPhotoAsync(Guid photoId, string photoName)
    {

        Photo? photo = await _photoRepository.GetAsync(photoId);
        if (photo == null)
        {
            return;
        }

        photo.Updated(photoName);
        await _photoRepository.UpdateAsync(photo);
    }

    public async Task DeletePhotoAsync(Guid photoId)
    {
        await _photoRepository.DeleteAsync(photoId);
    }


}

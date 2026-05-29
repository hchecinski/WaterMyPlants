using WaterMyPlants.Shared.Dtos;

namespace WaterMyPlants.Connector.Connectors;

public interface IPlantConnector
{
    Task<IEnumerable<PlantListItemDto>> GetPlantsAsync(CancellationToken cancellationToken = default);

    Task<PlantDetailsDto> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PlantDto> AddPlantAsync(CreatePlantDto plant, CancellationToken cancellationToken = default);

    Task DeletePlantAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdatePlantAsync(Guid id, UpdatePlantDto plant, CancellationToken cancellationToken = default);
}
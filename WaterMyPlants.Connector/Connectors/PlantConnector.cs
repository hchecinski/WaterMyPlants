using System.Net.Http.Json;
using WaterMyPlants.Connector.Connectors;
using WaterMyPlants.Connector.Models;
using WaterMyPlants.Shared.Dtos;

namespace WaterMyPlants.Connector;

public class PlantConnector : IPlantConnector
{
    private readonly HttpClient _httpClient;

    public PlantConnector(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<PlantDto> AddPlantAsync(CreatePlantDto plant, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeletePlantAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PlantDetailsDto> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PlantListItemDto>> GetPlantsAsync(CancellationToken cancellationToken = default)
    {
         var result = await _httpClient.GetFromJsonAsync<List<PlantListItemDto>>(
            "/plants",
            cancellationToken);

        return result ?? [];
    }

    public Task UpdatePlantAsync(Guid id, UpdatePlantDto plant, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

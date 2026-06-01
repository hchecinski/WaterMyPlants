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

    public async Task<PlantDto?> AddPlantAsync(CreatePlantDto plant, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/plant", plant);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PlantDto>();
    }

    public async Task DeletePlantAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _httpClient.DeleteAsync(
            $"/api/Plant/{id}",
            cancellationToken);
    }

    public async Task<PlantDetailsDto?> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var detail = await _httpClient.GetFromJsonAsync<PlantDetailsDto>(
            $"/api/Plant/{id}",
             cancellationToken);

        return detail;
    }

    public async Task<IEnumerable<PlantListItemDto>> GetPlantsAsync(CancellationToken cancellationToken = default)
    {
         var result = await _httpClient.GetFromJsonAsync<List<PlantListItemDto>>(
            "/api/Plant",
            cancellationToken);

        return result ?? [];
    }


    public Task UpdatePlantAsync(Guid id, UpdatePlantDto plant, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

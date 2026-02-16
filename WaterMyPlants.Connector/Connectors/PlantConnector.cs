using WaterMyPlants.Connector.Models;

namespace WaterMyPlants.Connector;

public class PlantConnector
{
    private readonly HttpClient _httpClient;

    public PlantConnector(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://localhost:5001/api/plants/");
    }

    public async Task<Result<IReadOnlyList<PlantModel>>> GetPlantsAsync()
    {
        // tutaj cała logika

    }
}

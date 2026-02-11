using Microsoft.AspNetCore.Mvc;
using WaterMyPlants.Application.Services;
using WaterMyPlants.Shared.Dtos;

namespace WaterMyPlants.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlantController : ControllerBase
{
    private readonly IPlantService _plantService;

    public PlantController(IPlantService plantService)
    {
        _plantService = plantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlantsAsync()
    {
        var plants = await _plantService.GetSortedAsync();

        return Ok(plants);
    }

    [HttpGet("{id:guid}", Name = nameof(GetDetailsAsync))]
    public async Task<ActionResult<PlantDetailsDto>> GetDetailsAsync(Guid id)
    {
        var plantDto = await _plantService.GetDetailsAsync(id);

        return Ok(plantDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlantAsync([FromBody] CreatePlantDto plant)
    {
        var id = await _plantService.AddAsync(plant);

        return CreatedAtRoute(nameof(GetDetailsAsync), new { id }, new { id });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePlantAsync(Guid id)
    {
        await _plantService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePlantAsync(Guid id, [FromBody] UpdatePlantDto plant)
    {
        await _plantService.UpdateAsync(id, plant);
        return NoContent();
    }
}

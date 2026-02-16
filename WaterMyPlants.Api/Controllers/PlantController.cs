using Microsoft.AspNetCore.Mvc;
using WaterMyPlants.Application.Services;
using WaterMyPlants.Domain.Models;
using WaterMyPlants.Shared.Dtos;

namespace WaterMyPlants.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantController : ControllerBase
{
    private readonly IPlantService _plantService;

    #region Plants
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
    #endregion

    #region Notes
    [HttpPost("{plantId:guid}/notes")]
    public async Task<IActionResult> AddNoteAsync(Guid plantId, [FromBody] CreateNoteDto note)
    {
        var noteId = await _plantService.AddNoteAsync(plantId, note);
        return CreatedAtRoute(nameof(GetNoteById), new { plantId = plantId, id = noteId }, new { id = noteId });
    }

    [HttpGet("{plantId:guid}/notes/{id:guid}", Name = nameof(GetNoteById))]
    public async Task<ActionResult<NoteDto>> GetNoteById(Guid plantId, Guid id)
    {
        var note = await _plantService.GetNoteById(plantId, id);
        return Ok(note);
    }

    [HttpGet("{plantId:guid}/notes")]
    public async Task<ActionResult<IReadOnlyList<NoteDto>>> GetNotesAsync(Guid plantId)
    {
        var notes = await _plantService.GetNotesAsync(plantId);
        return Ok(notes);
    }

    [HttpDelete("{plantId:guid}/notes/{id:guid}")]
    public async Task<IActionResult> DeleteNoteAsync(Guid plantId, Guid id)
    {
        await _plantService.DeleteNoteAsync(plantId, id);
        return NoContent();
    }

    [HttpPut("{plantId:guid}/notes/{id:guid}")]
    public async Task<IActionResult> UpdateNoteAsync(Guid plantId, Guid id, [FromBody] UpdateNoteDto note)
    {
        await _plantService.UpdateNoteAsync(plantId, id, note);
        return NoContent();
    }
    #endregion
}

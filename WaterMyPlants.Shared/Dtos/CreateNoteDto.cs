namespace WaterMyPlants.Shared.Dtos;

public class CreateNoteDto
{
    public Guid PlantId { get; set; }
    public string Text { get; set; } = string.Empty;
}

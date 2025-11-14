namespace WaterMyPlants.Application.Models;

public class UpdateNoteDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
}

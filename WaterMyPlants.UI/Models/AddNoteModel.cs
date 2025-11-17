namespace WaterMyPlants.UI.Models;

public class AddNoteModel
{
    public Guid PlantId { get; set; }
    public string Text { get; set; } = string.Empty;
}

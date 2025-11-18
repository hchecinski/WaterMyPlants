namespace WaterMyPlants.UI.Models;

public partial class NoteModel
{
    public Guid Id { get; set; }
    public Guid PlantId { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastSave { get; set; }
}
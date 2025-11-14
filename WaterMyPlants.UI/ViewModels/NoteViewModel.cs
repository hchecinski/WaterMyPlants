namespace WaterMyPlants.UI.ViewModels;

public class NoteViewModel
{
    public Guid Id { get; set; }
    public Guid PlantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastSave { get; set; }
    public string Text { get; set; } = string.Empty;
}

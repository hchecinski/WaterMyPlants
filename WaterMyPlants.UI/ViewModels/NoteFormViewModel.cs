namespace WaterMyPlants.UI.ViewModels;

public class NoteFormViewModel
{
    public Guid? Id { get; set; }
    public Guid PlantId { get; set; }
    public string Text { get; set; } = string.Empty;
}

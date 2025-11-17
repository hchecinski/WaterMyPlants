using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WaterMyPlants.UI.Models;

public partial class NoteModel : ObservableObject
{
    public Guid Id { get; set; }
    public Guid PlantId { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastSave { get; set; }

    [ObservableProperty]
    private bool _isSelected;

    [RelayCommand]
    private void Edit()
    {

    }
}
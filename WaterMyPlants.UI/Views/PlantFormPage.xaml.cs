using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Views;

public partial class PlantFormPage : ContentPage
{
	public PlantFormPage(PlantFormViewModel plantFormViewModel)
	{
		InitializeComponent();
		BindingContext = plantFormViewModel;
    }
}
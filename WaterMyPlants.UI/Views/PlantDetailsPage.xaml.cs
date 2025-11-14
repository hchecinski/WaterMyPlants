using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Views;

public partial class PlantDetailsPage : ContentPage
{
	public PlantDetailsPage(PlantDetailsViewModel plantDetailViewModel)
	{
		InitializeComponent();
		BindingContext = plantDetailViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}
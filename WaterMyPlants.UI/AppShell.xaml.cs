using WaterMyPlants.UI.Views;

namespace WaterMyPlants.UI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(PlantDetailsPage), typeof(PlantDetailsPage));
		Routing.RegisterRoute(nameof(PlantFormPage), typeof(PlantFormPage));
    }
}

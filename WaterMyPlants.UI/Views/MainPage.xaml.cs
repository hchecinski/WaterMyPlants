using WaterMyPlants.UI.ViewModels;

namespace WaterMyPlants.UI.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((MainViewModel)BindingContext).OnAppearing();
    }
}


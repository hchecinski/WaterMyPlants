using CommunityToolkit.Maui;
using WaterMyPlants.Infrastructure.DataBase;
using WaterMyPlants.UI.Factories;
using WaterMyPlants.UI.Services;
using WaterMyPlants.UI.ViewModels;
using WaterMyPlants.UI.Views;

namespace WaterMyPlants.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddUIServices(this IServiceCollection services)
    {
        services.AddTransient<MainPage>();
        services.AddTransient<MainViewModel>();

        services.AddTransient<PlantDetailsPage>();
        services.AddTransient<PlantDetailsViewModel>();

        services.AddTransient<PlantListItemView>();
        services.AddTransient<PlantListItemViewModel>();

        services.AddTransient<PlantFormPage>();
        services.AddTransient<PlantFormViewModel>();

        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IMapper, Mapper>();
        services.AddSingleton<IPlantViewModelFactory, PlantViewModelFactory>();

        services.AddSingleton<ISqliteConnectionFactory>(new SqliteConnectionFactory(Path.Combine(FileSystem.AppDataDirectory, "WaterMyPlantsDb.db")));

        return services;
    }
}

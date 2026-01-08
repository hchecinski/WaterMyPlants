using CommunityToolkit.Maui;
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

        services.AddTransient<NoteFormView>();
        services.AddTransient<NoteFormViewModel>();

        services.AddTransient<PlantFormPage>();
        services.AddTransient<PlantFormViewModel>();

        services.AddTransient<PhotoFormViewModel>();
        services.AddTransient<PhotoViewModel>();

        services.AddTransient<INavigationService, NavigationService>();
        services.AddTransient<IPlantService, PlantService>();
        services.AddTransient<INoteService, NoteService>();
        services.AddTransient<IPhotoService, PhotoService>();

        services.AddTransient<IMapper, Mapper>();
        services.AddTransient<IPlantViewModelFactory, PlantViewModelFactory>();
        services.AddTransient<INoteFactory, NoteFactory>();
        services.AddTransient<IPhotoFactory, PhotoFactory>();


        return services;
    }
}

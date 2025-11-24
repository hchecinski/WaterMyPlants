using Microsoft.Extensions.DependencyInjection;
using WaterMyPlants.Application.Services;

namespace WaterMyPlants.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IMapper, Mapper>();

        services.AddTransient<IPlantService, PlantService>();
        services.AddTransient<INoteService, NoteService>();
        services.AddTransient<IPhotoService, PhotoService>();

        return services;
    }
}

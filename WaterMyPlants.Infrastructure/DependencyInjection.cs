using Microsoft.Extensions.DependencyInjection;
using WaterMyPlants.Domain.Repositories;
using WaterMyPlants.Infrastructure.Repositories;

namespace WaterMyPlants.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IPlantRepository, PlantRepository>();
        services.AddTransient<INoteRepository, NoteRepository>();
        services.AddTransient<IPhotoRepository, PhotoRepository>();

        return services;
    }
}
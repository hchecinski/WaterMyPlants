using Microsoft.Extensions.DependencyInjection;
using WaterMyPlants.Domain.Repositories;
using WaterMyPlants.Infrastructure.EF.Repositories;

namespace WaterMyPlants.Infrastructure.EF.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IPlantRepository, PlantRepository>();
        services.AddTransient<INoteRepository, NoteRepository>();
        services.AddTransient<IPhotoRepository, PhotoRepository>();
        services.AddTransient<IMapper, Mapper>();

        return services;
    }
}

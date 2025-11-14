using Microsoft.Extensions.DependencyInjection;
using WaterMyPlants.Application.Services;

namespace WaterMyPlants.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IPlantService, PlantServiceFake>();

        return services;
    }
}

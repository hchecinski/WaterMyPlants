using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using WaterMyPlants.Application;
using WaterMyPlants.Infrastructure;
using WaterMyPlants.UI.Services;
using WaterMyPlants.UI.ViewModels;
using WaterMyPlants.UI.Views;

namespace WaterMyPlants.UI;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit();
     
#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddUIServices();
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();

        return builder.Build();
    }
}
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using WaterMyPlants.Application;
using WaterMyPlants.Connector;

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
        builder.Services.AddHttpClient("clientName", client =>
        {
            client.BaseAddress = new Uri("http://10.0.2.2:5008/");
            client.Timeout = TimeSpan.FromSeconds(10);
        });
        return builder.Build();
    }

}
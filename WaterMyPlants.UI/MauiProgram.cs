using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using WaterMyPlants.Application;
using WaterMyPlants.Infrastructure;

namespace WaterMyPlants.UI;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        SQLitePCL.Batteries_V2.Init();

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

        Task.Run(InitializeDatabase).Wait();

        builder.Services.AddUIServices();
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();

        return builder.Build();
    }

    private static async Task InitializeDatabase()
    {
        var dbName = "WaterMyPlantsDb.db";
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, dbName);

        if (!File.Exists(dbPath))
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(dbName);
            using var outStream = File.Create(dbPath);
            await stream.CopyToAsync(outStream);
        }
    }
}
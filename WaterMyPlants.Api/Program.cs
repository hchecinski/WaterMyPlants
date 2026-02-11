using Microsoft.EntityFrameworkCore;
using Serilog;
using WaterMyPlants.Api.Helpers;
using WaterMyPlants.Infrastructure.EF.Configurations;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();

builder.Services.AddDbContext<WaterMyPlantsDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PlantConnectionString")));

DependencyInjection.AddInfrastructureServices(builder.Services);
WaterMyPlants.Application.DependencyInjection.AddApplicationServices(builder.Services);

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
});

app.MapControllers();

app.Run();

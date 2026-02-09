using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WaterMyPlants.Infrastructure.EF.Configurations;

public class WaterMyPlantsDbContextFactory : IDesignTimeDbContextFactory<WaterMyPlantsDbContext>
{
    public WaterMyPlantsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WaterMyPlantsDbContext>();

        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=appdb;Username=appuser;Password=apppassword");

        return new WaterMyPlantsDbContext(optionsBuilder.Options);
    }
}

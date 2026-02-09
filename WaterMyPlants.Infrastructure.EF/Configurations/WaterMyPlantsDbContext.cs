using Microsoft.EntityFrameworkCore;
using WaterMyPlants.Infrastructure.EF.Entities;

namespace WaterMyPlants.Infrastructure.EF.Configurations;

public class WaterMyPlantsDbContext : DbContext
{
    public DbSet<Plant> Plants => Set<Plant>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Photo> Photos => Set<Photo>();

    public WaterMyPlantsDbContext(DbContextOptions<WaterMyPlantsDbContext> options) : base(options)
    {
    }
}

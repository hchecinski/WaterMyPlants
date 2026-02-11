using Microsoft.EntityFrameworkCore;
using WaterMyPlants.Domain.Models;

namespace WaterMyPlants.Infrastructure.EF.Configurations;

public class WaterMyPlantsDbContext : DbContext
{
    public DbSet<Plant> Plants => Set<Plant>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Photo> Photos => Set<Photo>();

    public WaterMyPlantsDbContext(DbContextOptions<WaterMyPlantsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigurePlant(modelBuilder);
        ConfigureNote(modelBuilder);
        ConfigurePhotos(modelBuilder);
    }

    private void ConfigurePhotos(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Photo>();

        builder.ToTable("Photos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Path).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property<Guid>("PlantId").IsRequired();

        builder.HasIndex(p => p.Name).IsUnique();
    }

    private void ConfigureNote(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Note>();

        builder.ToTable("Notes");
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Text).IsRequired().HasMaxLength(500);
        builder.Property(n => n.CreatedAt).IsRequired();
        builder.Property<Guid>("PlantId").IsRequired();
    }

    private void ConfigurePlant(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Plant>();

        builder.ToTable("Plants");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);   
        builder.Property(p => p.Localization).HasMaxLength(200);
        builder.Property(p => p.Description).HasMaxLength(1000);
        builder.Property(p => p.WaterIntervalDays).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();

        builder.HasMany<Note>("_notes").WithOne().HasForeignKey("PlantId").OnDelete(DeleteBehavior.Cascade);
        builder.HasMany<Photo>("_photos").WithOne().HasForeignKey("PlantId").OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(Plant.Notes))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Plant.Photos))!.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(p => p.Name).IsUnique();
    }
}

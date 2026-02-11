using Microsoft.EntityFrameworkCore;
using WaterMyPlants.Domain.Models;
using WaterMyPlants.Domain.Repositories;
using WaterMyPlants.Infrastructure.EF.Configurations;

namespace WaterMyPlants.Infrastructure.EF.Repositories;

public class PlantRepository : IPlantRepository
{
    private readonly WaterMyPlantsDbContext _dbContext;

    public PlantRepository(WaterMyPlantsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Plant plant)
    {
        _dbContext.Plants.Add(plant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Plant>> GetAllAsync()
    {
        return await _dbContext.Plants.Include(p => p.Notes).Include(p => p.Photos).ToListAsync();
    }

    public async Task<Plant?> GetAsync(Guid id)
    {
        return await _dbContext.Plants.Include(p => p.Notes).Include(p => p.Photos).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task RemoveAsync(Plant plant)
    {
        _dbContext.Plants.Remove(plant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

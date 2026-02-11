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

    public async Task AddNoteAsync(Note note)
    {
        _dbContext.Entry(note).State = EntityState.Added;
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

    public async Task<Note?> GetNoteByIdAsync(Guid plantId, Guid noteId)
    {
        return await _dbContext.Notes.FirstOrDefaultAsync(n => n.Id == noteId && n.PlantId == plantId);
    }

    public async Task<IReadOnlyList<Note>> GetNotesAsync(Guid plantId)
    {
        return await _dbContext.Notes.Where(n => n.PlantId == plantId).ToListAsync();
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

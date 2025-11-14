using WaterMyPlants.Application.Models;

namespace WaterMyPlants.Application.Services;

public class PlantServiceFake : IPlantService
{

    private readonly List<PlantListItemDto> _plants;
    private readonly List<PlantDetailsDto> _plantDetails;

    public PlantServiceFake()
    {
        var plant1Id = Guid.NewGuid();
        var plant2Id = Guid.NewGuid();
        var plant3Id = Guid.NewGuid();

        _plants = new List<PlantListItemDto>
        {
            new PlantListItemDto
            {
                Id = plant1Id,
                Name = "Monstera",
                Localization = "Salon",
                Description = "Lubi cień i wilgotne powietrze",
                LastNote = "Podlana 2 dni temu",
                DaysRemaining = 3
            },
            new PlantListItemDto
            {
                Id = plant2Id,
                Name = "Aloes",
                Localization = "Kuchnia",
                Description = "Roślina lecznicza",
                LastNote = "Postawić w jaśniejszym miejscu",
                DaysRemaining = 7
            },
            new PlantListItemDto
            {
                Id = plant3Id,
                Name = "Epipremnum",
                Localization = "Sypialnia",
                Description = "Wymaga mało światła",
                LastNote = "Nowy liść pojawił się wczoraj",
                DaysRemaining = 5
            }
        };

        _plantDetails = new List<PlantDetailsDto>
        {
            new PlantDetailsDto
            {
                Id = plant1Id,
                Name = "Monstera",
                Localization = "Salon",
                Description = "Lubi cień i wilgotne powietrze",
                CreatedAt = DateTime.UtcNow.AddDays(-100),
                LastWaterAt = DateTime.UtcNow.AddDays(-2),
                NextWaterAt = DateTime.UtcNow.AddDays(3),

                Notes = new List<NoteDto>
                {
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) }
                },

                Photos = new List<PhotoDto>
                {
                    new PhotoDto { Id = Guid.NewGuid(), PlantId = plant1Id, Path = "sample1.jpg", CreatedAt = DateTime.UtcNow.AddDays(-15) },
                    new PhotoDto { Id = Guid.NewGuid(), PlantId = plant1Id, Path = "sample2.jpg", CreatedAt = DateTime.UtcNow.AddDays(-30) }
                }
            },

            new PlantDetailsDto
            {
                Id = plant2Id,
                Name = "Aloes",
                Localization = "Kuchnia",
                Description = "Roślina lecznicza",
                CreatedAt = DateTime.UtcNow.AddDays(-200),
                LastWaterAt = DateTime.UtcNow.AddDays(-5),
                NextWaterAt = DateTime.UtcNow.AddDays(2),

                Notes = new List<NoteDto>
                {
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant2Id, Text = "Mało wody", CreatedAt = DateTime.UtcNow.AddDays(-5), LastSave = DateTime.UtcNow },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant3Id, Text = "Super rośnie!", CreatedAt = DateTime.UtcNow.AddDays(-3), LastSave = DateTime.UtcNow.AddDays(-1)  },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) },
                                        new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Podlana", CreatedAt = DateTime.UtcNow.AddDays(-2), LastSave = DateTime.UtcNow.AddDays(-2) },
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant1Id, Text = "Przestawiona bliżej okna", CreatedAt = DateTime.UtcNow.AddDays(-10), LastSave = DateTime.UtcNow.AddDays(-6) }

                },

                Photos = new List<PhotoDto>
                {
                    new PhotoDto { Id = Guid.NewGuid(), PlantId = plant2Id, Path = "aloe1.jpg", CreatedAt = DateTime.UtcNow.AddDays(-50) }
                }
            },

            new PlantDetailsDto
            {
                Id = plant3Id,
                Name = "Epipremnum",
                Localization = "Sypialnia",
                Description = "Wymaga mało światła",
                CreatedAt = DateTime.UtcNow.AddDays(-300),
                LastWaterAt = DateTime.UtcNow.AddDays(-1),
                NextWaterAt = DateTime.UtcNow.AddDays(4),

                Notes = new List<NoteDto>
                {
                    new NoteDto { Id = Guid.NewGuid(), PlantId = plant3Id, Text = "Super rośnie!", CreatedAt = DateTime.UtcNow.AddDays(-3), LastSave = DateTime.UtcNow.AddDays(-1)  }
                },

                Photos = new List<PhotoDto>()
            }
        };
    }

    public Task<IReadOnlyList<PlantListItemDto>> GetSortedAsync()
    {
        var sorted = _plants
            .OrderBy(p => p.DaysRemaining)
            .ToList();

        return Task.FromResult<IReadOnlyList<PlantListItemDto>>(sorted);
    }

    public Task<PlantDetailsDto> GetDetailsAsync(Guid id)
    {
        var plant = _plantDetails.FirstOrDefault(p => p.Id == id);
        if (plant == null)
            throw new KeyNotFoundException($"Plant {id} not found");

        return Task.FromResult(plant);
    }

    // Operacje mutujące nie są potrzebne w fake — można zostawić NotImplemented

    public Task<Guid> AddAsync(PlantDto plant)
        => throw new NotImplementedException();

    public Task UpdateAsync(PlantDto plant)
        => throw new NotImplementedException();

    public Task DeleteAsync(Guid id)
        => throw new NotImplementedException();

    public Task WaterNowAsync(Guid id, DateTime nowUtc)
        => throw new NotImplementedException();

    public Task UndoWaterAsync(Guid id, DateTime previousUtc)
        => throw new NotImplementedException();
}

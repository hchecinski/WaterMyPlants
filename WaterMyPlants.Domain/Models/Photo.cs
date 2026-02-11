namespace WaterMyPlants.Domain.Models;

public sealed class Photo  
{
    public Guid Id { get; private set; }
    public Guid PlantId { get; private set; }
    public string Path { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }
    public Plant Plant { get; private set; } = null!;

    public Photo() { }
    public Photo(Guid id, DateTime createAt)
    {
        Id = id;
        CreatedAt = createAt;
    }

    public void Updated(string newName)
    {
        Name = newName;
        LastUpdatedAt = DateTime.UtcNow;
    }

    internal void Update(string name, DateTime updatedAt)
    {
        throw new NotImplementedException();
    }

    internal static Photo Create(Guid photoId, Guid plantId, string path, string name, DateTime createAt)
    {
        throw new NotImplementedException();
    }
}

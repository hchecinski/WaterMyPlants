namespace WaterMyPlants.Domain.Models;

public abstract class Entity
{
    public string Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
}

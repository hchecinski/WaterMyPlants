namespace WaterMyPlants.Domain.Models;

public abstract class UpdatableEntity : Entity
{
    public DateTime? LastUpdatedAt { get; protected set; }
}

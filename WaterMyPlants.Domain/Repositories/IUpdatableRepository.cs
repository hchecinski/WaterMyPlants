namespace WaterMyPlants.Domain.Repositories;

public interface IUpdatableRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    Task UpdateAsync(TEntity entity);
}

namespace WaterMyPlants.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task InsertAsync(TEntity entity);
    Task DeleteAsync(Guid id);
}

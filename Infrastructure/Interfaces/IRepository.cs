namespace DataAccess.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
{
    IEnumerable<TEntity> GetAll();
    TEntity? GetById(int id);
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(int id);
    void Delete(TEntity entity);
    void Save();
}

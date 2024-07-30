using MotoApp.Data.Entities;

namespace MotoApp.Data.Repositories;

public interface IWriteRepository<in T> where T : class, IEntity
{
    void Add(T entity);
    void Remove(T entity);
    void Save();
}

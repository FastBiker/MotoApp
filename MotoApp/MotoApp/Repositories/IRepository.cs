namespace MotoApp.Repositories;

using MotoApp.Entities;

public interface IRepository<T> where T : class, IEntity
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Remove(T entity);
    void Save();
}

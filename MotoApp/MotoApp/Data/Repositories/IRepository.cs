namespace MotoApp.Data.Repositories;

using MotoApp.Data.Entities;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{
}

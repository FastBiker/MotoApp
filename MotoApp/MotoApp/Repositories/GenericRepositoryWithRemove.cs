using MotoApp.Entities;

namespace MotoApp.Repositories
{
    public class GenericRepositoryWithRemove<TEntity, TKey> : GenericRepository<TEntity, TKey> 
        where TEntity : class, IEntitiy, new()
        where TKey : struct
    {
        public void Remove(TEntity item) 
        {
            _items.Remove(item);
        }
    }
}

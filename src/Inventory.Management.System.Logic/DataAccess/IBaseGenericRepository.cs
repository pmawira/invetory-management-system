
namespace Inventory.Management.System.Logic.DataAccess
{
    public interface IBaseGenericRepository<T>
    {
        public Task Add(T entity);
        public void Delete(T entity);
        public IQueryable<T> GetSet();      
        public Task Update(int id, T entity, CancellationToken cancellationToken);
    }
}

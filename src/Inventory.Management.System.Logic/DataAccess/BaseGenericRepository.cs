


using Microsoft.EntityFrameworkCore;

namespace Inventory.Management.System.Logic.DataAccess
{
    public class BaseGenericRepository<T> : IBaseGenericRepository<T> where T : class
    {
        protected readonly IUnitOfWork Uow;
        public BaseGenericRepository(IUnitOfWork uow)
        {
            Uow = uow;
        }
       public async Task Add(T entity)
        {
            await Uow.Context.Set<T>().AddAsync(entity);
        }
        public void Delete(T entity)
        {
            var dbSet = Uow.Context.Set<T>();

            // Ensure the entity is attached before removing
            if (Uow.Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public IQueryable<T> GetSet()
        {
            var res = Uow.Context.Set<T>()
                .AsNoTracking();

            return res;
        }

        public void Update(int id, T entity, CancellationToken cancellationToken)
        {
            Uow.Context.Set<T>().Update(entity);
        }
    }
}

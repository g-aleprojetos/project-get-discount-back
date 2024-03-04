using Microsoft.EntityFrameworkCore;
using project_get_discount_back.Common;
using project_get_discount_back.Context;
using project_get_discount_back.Interfaces;

namespace project_get_discount_back.Repositories
{
    public class BaseRepository<T>(DataContext context) : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataContext Context = context;

        public void Create(T entity)
        {
            Context.Add(entity);
        }

        public async Task<T?> Get(Guid id, CancellationToken cancellationToken)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await Context.Set<T>().ToListAsync(cancellationToken);
        }

        public void Delete(T entity)
        {
            entity.DeletedAt = new DateTimeOffset(DateTime.UtcNow);
            Context.Remove(entity);
        }

        void IBaseRepository<T>.Update(T entity)
        {
            entity.DeletedAt = new DateTimeOffset(DateTime.UtcNow);
            Context.Update(entity);
        }

        void IBaseRepository<T>.DeleteLogic(T entity)
        {
            entity.DeletedAt = new DateTimeOffset(DateTime.UtcNow);
            entity.Deleted = true;
            Context.Update(entity);
        }
    }
}

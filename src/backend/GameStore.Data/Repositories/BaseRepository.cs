using GameStore.Data.Entities;
using GameStore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameStore.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> dbSet;
        public DbContext Context { get; }

        public BaseRepository(DbContext db)
        {
            Context = db;
            dbSet = db.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null!, bool disableTracking = false)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);
            if (disableTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id, bool disableTracking = false)
        {
            IQueryable<T> query = dbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(T model)
        {
            await dbSet.AddAsync(model);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != null)
                dbSet.Remove(entity);
        }

        public void Delete(T model)
        {
            dbSet.Remove(model);
        }

        public void Update(T model)
        {
            dbSet.Update(model);
        }

        public async Task<IEnumerable<T>> GetPagedListAsync(int page = 1, int pageSize = 25)
        {
            return await dbSet.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}

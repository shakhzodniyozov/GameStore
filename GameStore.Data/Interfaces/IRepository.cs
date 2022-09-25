using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameStore.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbContext Context { get; }
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null!, bool disableTracking = false);
        Task<T?> GetByIdAsync(Guid id, bool disableTracking = false);
        Task<IEnumerable<T>> GetPagedListAsync(int page = 1, int pageSize = 25);
        Task CreateAsync(T model);
        void Update(T model);
        Task DeleteAsync(Guid id);
        void Delete(T model);
    }
}

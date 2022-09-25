using GameStore.Data.Entities;
using System.Linq.Expressions;

namespace GameStore.BusinessLogic.Interfaces
{
    public interface IService<T, TModel> where T : BaseEntity
    {
        Task<IEnumerable<TModel>> GetAsync(Expression<Func<T, bool>> filter = null!, bool disableTracking = false);
        Task<IEnumerable<TModel>> GetPagedListAsync(int page = 1, int pageSize = 25);
        Task<TModel> GetByIdAsync(Guid id, bool disableTracking = false);
        Task CreateAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task Delete(Guid id);
        void DeleteAsync(TModel model);
    }
}

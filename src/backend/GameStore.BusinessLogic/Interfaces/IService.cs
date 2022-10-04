using GameStore.Data.Entities;
using System.Linq.Expressions;

namespace GameStore.BusinessLogic.Interfaces
{
    public interface IService<T, TDto, TCreateDto, TUpdateDto> where T : BaseEntity
    {
        Task<IEnumerable<TDto>> GetAsync(Expression<Func<T, bool>> filter, bool disableTracking);
        Task<IEnumerable<TDto>> GetPagedListAsync(int page, int pageSize);
        Task<TDto> GetByIdAsync(Guid id, bool disableTracking = false);
        Task CreateAsync(TCreateDto dto);
        Task UpdateAsync(TUpdateDto dto);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(TDto model);
    }
}

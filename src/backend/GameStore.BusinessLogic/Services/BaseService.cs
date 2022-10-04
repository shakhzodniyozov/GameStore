using AutoMapper;
using GameStore.BusinessLogic.Interfaces;
using GameStore.Data.Entities;
using GameStore.Data.Interfaces;
using System.Linq.Expressions;

namespace GameStore.BusinessLogic.Services
{
    public class BaseService<T, TDto, TCreateDto, TUpdateDto> : IService<T, TDto, TCreateDto, TUpdateDto> where T : BaseEntity
    {
        private readonly IRepository<T> repo;
        private readonly IMapper mapper;

        public BaseService(IRepository<T> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public virtual async Task CreateAsync(TCreateDto model)
        {
            await repo.CreateAsync(mapper.Map<TCreateDto, T>(model));
            await repo.Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await repo.DeleteAsync(id);
            await repo.Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TDto model)
        {
            repo.Delete(mapper.Map<TDto, T>(model));
            await repo.Context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TDto>> GetAsync(Expression<Func<T, bool>> filter = null!, bool disableTracking = false)
        {
            return mapper.Map<IEnumerable<TDto>>(await repo.GetAsync(filter, disableTracking));
        }

        public virtual async Task<TDto> GetByIdAsync(Guid id, bool disableTracking = false)
        {
            return mapper.Map<TDto>(await repo.GetByIdAsync(id, disableTracking));
        }

        public virtual async Task<IEnumerable<TDto>> GetPagedListAsync(int page = 1, int pageSize = 25)
        {
            return mapper.Map<IEnumerable<TDto>>(await repo.GetPagedListAsync(page, pageSize));
        }

        public virtual async Task UpdateAsync(TUpdateDto model)
        {
            repo.Update(mapper.Map<T>(model));
            await repo.Context.SaveChangesAsync();
        }
    }
}

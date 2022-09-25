using AutoMapper;
using GameStore.BusinessLogic.Interfaces;
using GameStore.Data.Entities;
using GameStore.Data.Interfaces;
using System.Linq.Expressions;

namespace GameStore.BusinessLogic.Services
{
    public class BaseService<T, TModel> : IService<T, TModel> where T : BaseEntity
    {
        private readonly IRepository<T> repo;
        private readonly IMapper mapper;

        public BaseService(IRepository<T> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public virtual async Task CreateAsync(TModel model)
        {
            await repo.CreateAsync(mapper.Map<TModel, T>(model));
            await repo.Context.SaveChangesAsync();
        }

        public virtual async Task Delete(Guid id)
        {
            await repo.DeleteAsync(id);
            await repo.Context.SaveChangesAsync();
        }

        public virtual void DeleteAsync(TModel model)
        {
            repo.Delete(mapper.Map<TModel, T>(model));

        }

        public virtual async Task<IEnumerable<TModel>> GetAsync(Expression<Func<T, bool>> filter = null!, bool disableTracking = false)
        {
            return mapper.Map<IEnumerable<TModel>>(await repo.GetAsync(filter, disableTracking));
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id, bool disableTracking = false)
        {
            return mapper.Map<TModel>(await repo.GetByIdAsync(id, disableTracking));
        }

        public virtual async Task<IEnumerable<TModel>> GetPagedListAsync(int page = 1, int pageSize = 25)
        {
            return mapper.Map<IEnumerable<TModel>>(await repo.GetPagedListAsync(page, pageSize));
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            repo.Update(mapper.Map<T>(model));
            await repo.Context.SaveChangesAsync();
        }
    }
}

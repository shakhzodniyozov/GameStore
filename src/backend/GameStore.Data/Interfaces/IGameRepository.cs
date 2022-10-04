using GameStore.Data.Entities;

namespace GameStore.Data.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game?> GetByIdWithDetailsAsync(Guid id, bool disableTracking = false);
        Task<IEnumerable<Game>> GetPagedListWithDetailsAsync(int page = 1, int pageSize = 25);
    }
}

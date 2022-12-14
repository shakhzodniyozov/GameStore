using GameStore.Data.Entities;

namespace GameStore.Data.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetGenresWithDetailsAsync();
    }
}

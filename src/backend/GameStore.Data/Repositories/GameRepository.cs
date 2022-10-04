using GameStore.Data.Data;
using GameStore.Data.Entities;
using GameStore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.Data.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(GameStoreDBContext context) : base(context)
        {

        }

        public async Task<Game?> GetByIdWithDetailsAsync(Guid id, bool disableTracking = false)
        {
            IQueryable<Game> query = dbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            return await query.Include(g => g.Genres).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetPagedListWithDetailsAsync(int page = 1, int pageSize = 25)
        {
            return await dbSet.Include(g => g.Genres)
                              .Skip((page - 1) * 25)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public override async Task<IEnumerable<Game>> GetAsync(Expression<Func<Game, bool>> filter = null!, bool disableTracking = false)
        {
            IQueryable<Game> query = dbSet.Include(x => x.Genres).ThenInclude(x => x.Parent);

            if (filter != null)
                query = query.Where(filter);
            if (disableTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }
    }
}

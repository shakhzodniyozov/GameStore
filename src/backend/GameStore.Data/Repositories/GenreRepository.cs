using GameStore.Data.Data;
using GameStore.Data.Entities;
using GameStore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameStore.Data.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(GameStoreDBContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Genre>> GetGenresWithDetailsAsync()
        {
            return await dbSet.Include(g => g.ChildGenres).Where(g => g.Parent == null).OrderBy(x => x.Name).ToListAsync();
        }

        public override async Task<IEnumerable<Genre>> GetAsync(Expression<Func<Genre, bool>> filter = null!, bool disableTracking = false)
        {
            IQueryable<Genre> query = dbSet.Include(x => x.Games).Include(x => x.ChildGenres).ThenInclude(x => x.Games);

            if (filter != null)
                query = query.Where(filter);
            if (disableTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }
    }
}

using GameStore.Data.Interfaces;
using GameStore.Data.Repositories;

namespace GameStore.Data.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDBContext context;
        private GameRepository gameRepository = null!;
        private GenreRepository genreRepository = null!;

        public UnitOfWork(GameStoreDBContext context)
        {
            this.context = context;
        }

        public IGameRepository GameRepository
        {
            get
            {
                return gameRepository ??= new GameRepository(context);
            }
        }

        public IGenreRepository GenreRepository
        {
            get
            {
                return genreRepository ??= new GenreRepository(context);
            }
        }

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}

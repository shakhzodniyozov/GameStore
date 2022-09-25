using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Data
{
    public class GameStoreDBContext : DbContext
    {
        public GameStoreDBContext(DbContextOptions<GameStoreDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(GameStoreDBContext).Assembly);
        }
    }
}

using GameStore.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Data.Data
{
    public static class DBInitializer
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<GameStoreDBContext>();

            AddGenres(db);
        }

        private static void AddGenres(GameStoreDBContext db)
        {
            if (db.Set<Genre>().Any())
                return;
            
            var genres = new List<Genre>();

            var strategy = new Genre() { Id = Guid.NewGuid(), Name = "Strategy" };
            var action = new Genre() { Id = Guid.NewGuid(), Name = "Action" };

            strategy.ChildGenres = new List<Genre>()
            {
                new() { Id = Guid.NewGuid(), Name = "Rally", Parent = strategy, ParentId = strategy.Id },
                new() { Id = Guid.NewGuid(), Name = "Arcade", Parent = strategy, ParentId = strategy.Id },
                new() { Id = Guid.NewGuid(), Name = "Formula", Parent = strategy, ParentId = strategy.Id },
                new() { Id = Guid.NewGuid(), Name = "Off-road", Parent = strategy, ParentId = strategy.Id }
            };

            action.ChildGenres = new List<Genre>()
            {
                new() { Id = Guid.NewGuid(), Name = "FPS", Parent = action, ParentId = action.Id },
                new() { Id = Guid.NewGuid(), Name = "TPS", Parent = action, ParentId = action.Id },
                new() { Id = Guid.NewGuid(), Name = "Misc", Parent = action, ParentId = action.Id }
            };

            genres.AddRange(new Genre[]
            {
                strategy, action,
                new() { Id = Guid.NewGuid(), Name = "RPG"},
                new() { Id = Guid.NewGuid(), Name = "Sports"},
                new() { Id = Guid.NewGuid(), Name = "Races"},
                new() { Id = Guid.NewGuid(), Name = "Adventure"},
                new() { Id = Guid.NewGuid(), Name = "Puzzle & skill"},
                new() { Id = Guid.NewGuid(), Name = "Other"}
            });

            db.Set<Genre>().AddRangeAsync(genres).GetAwaiter().GetResult();
            db.SaveChangesAsync();
        }
    }
}

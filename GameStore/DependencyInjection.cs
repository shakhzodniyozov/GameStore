using GameStore.BusinessLogic.Interfaces;
using GameStore.BusinessLogic.Services;
using GameStore.Data.Data;
using GameStore.Data.Interfaces;
using GameStore.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameStore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGenreService, GenreService>();

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GameStoreDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("sqlServer")));

            return services;
        }
    }
}

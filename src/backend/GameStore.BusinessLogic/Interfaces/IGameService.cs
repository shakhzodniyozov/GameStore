using GameStore.BusinessLogic.Dtos;
using GameStore.Data.Entities;

namespace GameStore.BusinessLogic.Interfaces
{
    public interface IGameService : IService<Game, GameDto, CreateGameDto, UpdateGameDto>
    {
        Task<GameDto> GetByIdWithDetailsAsync(Guid id);
        Task<IEnumerable<GameDto>> GetPagedListWithDetailsAsync(int page = 1, int pageSize = 25);
        Task<IEnumerable<GameDto>> GetFilteredGamesAsync(FilterGamesDto filter);
    }
}

using GameStore.BusinessLogic.Dtos;
using GameStore.Data.Entities;

namespace GameStore.BusinessLogic.Interfaces
{
    public interface IGenreService : IService<Genre, GenreDto, CreateGenreDto, UpdateGenreDto>
    {
        Task<IEnumerable<GenreWithDetailsDto>> GetGenresWithDetailsAsync();
    }
}

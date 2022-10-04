using AutoMapper;
using GameStore.BusinessLogic.Interfaces;
using GameStore.BusinessLogic.Dtos;
using GameStore.Data.Entities;
using GameStore.Data.Interfaces;

namespace GameStore.BusinessLogic.Services
{
    public class GenreService : BaseService<Genre, GenreDto, CreateGenreDto, UpdateGenreDto>, IGenreService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public GenreService(IUnitOfWork uow, IMapper mapper) : base(uow.GenreRepository, mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GenreWithDetailsDto>> GetGenresWithDetailsAsync()
        {
            return mapper.Map<IEnumerable<GenreWithDetailsDto>>(await uow.GenreRepository.GetGenresWithDetailsAsync());
        }
    }
}

using AutoMapper;
using GameStore.BusinessLogic.Interfaces;
using GameStore.BusinessLogic.Models;
using GameStore.Data.Entities;
using GameStore.Data.Interfaces;

namespace GameStore.BusinessLogic.Services
{
    public class GenreService : BaseService<Genre, GenreModel>, IGenreService
    {
        public GenreService(IUnitOfWork uow, IMapper mapper) : base(uow.GenreRepository, mapper)
        {

        }
    }
}

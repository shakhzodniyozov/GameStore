using AutoMapper;
using GameStore.BusinessLogic.Interfaces;
using GameStore.BusinessLogic.Models;
using GameStore.Data.Entities;
using GameStore.Data.Interfaces;

namespace GameStore.BusinessLogic.Services
{
    public class GameService : BaseService<Game, GameModel>, IGameService
    {
        public GameService(IUnitOfWork uow, IMapper mapper) : base(uow.GameRepository, mapper)
        {

        }
    }
}

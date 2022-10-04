using AutoMapper;
using GameStore.BusinessLogic.Dtos;
using GameStore.Data.Entities;

namespace GameStore.BusinessLogic.AutomapperProfiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameDto>();

            CreateMap<CreateGameDto, Game>();

            CreateMap<UpdateGameDto, Game>();
        }
    }
}

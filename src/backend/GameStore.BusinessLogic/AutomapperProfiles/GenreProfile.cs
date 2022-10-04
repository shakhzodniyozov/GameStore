using AutoMapper;
using GameStore.BusinessLogic.Dtos;
using GameStore.Data.Entities;

namespace GameStore.BusinessLogic.AutomapperProfiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDto>()
                .ForMember(d => d.Parent, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Name : null));

            CreateMap<CreateGenreDto, Genre>();

            CreateMap<UpdateGenreDto, GenreDto>();

            CreateMap<Genre, GenreWithDetailsDto>()
                .ForMember(d => d.Children, opt => opt.MapFrom((genre, genreWithDetailsDto) =>
                {
                    if (genre.ChildGenres != null)
                        return genre.ChildGenres.Select(x => new GenreDto() { Id = x.Id, Name = x.Name, Parent = x.Parent?.Name });
                    return null;
                }));
        }
    }
}

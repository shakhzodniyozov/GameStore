using AutoMapper;
using GameStore.BusinessLogic.Interfaces;
using GameStore.BusinessLogic.Dtos;
using GameStore.Data.Entities;
using GameStore.Data.Interfaces;

namespace GameStore.BusinessLogic.Services
{
    public class GameService : BaseService<Game, GameDto, CreateGameDto, UpdateGameDto>, IGameService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly ImageService imageService;

        public GameService(IUnitOfWork uow, IMapper mapper, ImageService imageService) : base(uow.GameRepository, mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.imageService = imageService;
        }

        public override async Task CreateAsync(CreateGameDto dto)
        {
            var newGame = mapper.Map<Game>(dto);
            newGame.ImagePath = await imageService.SetImageToGame(newGame, dto.ImageAsBase64);
            foreach (var genreId in dto.GenreIds)
            {
                var genre = await uow.GenreRepository.GetByIdAsync(genreId);
                if (genre == null)
                    throw new BusinessLogicException("Genre with provided Id was now found while creating a game.", 400);
                newGame.Genres.Add(genre);
            }
            await uow.GameRepository.CreateAsync(newGame);
            await uow.SaveAsync();
        }

        public async Task<GameDto> GetByIdWithDetailsAsync(Guid id)
        {
            return mapper.Map<GameDto>(await uow.GameRepository.GetByIdWithDetailsAsync(id));
        }

        public async Task<IEnumerable<GameDto>> GetPagedListWithDetailsAsync(int page = 1, int pageSize = 25)
        {
            return mapper.Map<IEnumerable<GameDto>>(await uow.GameRepository.GetPagedListWithDetailsAsync(page, pageSize));
        }

        public override async Task UpdateAsync(UpdateGameDto dto)
        {
            var game = await uow.GameRepository.GetByIdWithDetailsAsync(dto.Id);
            if (game == null)
                throw new BusinessLogicException("Game with provided Id was not found.", 400);

            mapper.Map(dto, game);
            var genresToBeAdded = dto.GenreIds.Except(game.Genres.Select(g => g.Id)).ToList();
            var genresToBoRemoved = game.Genres.Select(g => g.Id).Except(dto.GenreIds).ToList();

            await AddGenres(genresToBeAdded, game);
            RemoveGenres(genresToBoRemoved, game);

            if (!string.IsNullOrEmpty(dto.ImageAsBase64))
            {
                imageService.DeleteGameImage(game);
                await imageService.SetImageToGame(game, dto.ImageAsBase64);
            }

            uow.GameRepository.Update(game);
            await uow.SaveAsync();
        }

        public override async Task DeleteAsync(Guid id)
        {
            var game = await uow.GameRepository.GetByIdAsync(id);
            if (game == null)
                throw new BusinessLogicException("Game with provided id was not found while deleting a game", 400);
            
            imageService.DeleteGameImage(game);
            await uow.GameRepository.DeleteAsync(id);
            await uow.SaveAsync();
        }

        private async Task AddGenres(IEnumerable<Guid>? genresToBeAdded, Game game)
        {
            if (genresToBeAdded == null)
                return;

            foreach (var genreId in genresToBeAdded)
            {
                var genre = await uow.GenreRepository.GetByIdAsync(genreId);
                if (genre == null)
                    throw new BusinessLogicException("Genre with provided Id was not found while updating a game.", 400);

                game.Genres.Add(genre);
            }
        }

        private void RemoveGenres(IEnumerable<Guid>? genresToBeRemoved, Game game)
        {
            if (genresToBeRemoved == null)
                return;

            foreach (var genreId in genresToBeRemoved)
            {
                var genre = game.Genres.SingleOrDefault(g => g.Id == genreId);
                if (genre == null)
                    throw new BusinessLogicException("Genre with provided Id was not found while updating a game.", 400);

                game.Genres.Remove(genre);
            }
        }

        public async Task<IEnumerable<GameDto>> GetFilteredGamesAsync(FilterGamesDto filter)
        {
            var genres = await uow.GenreRepository.GetAsync(disableTracking: true);
            var games = new List<Game>();

            if (filter.Genres.Any())
                genres = genres.Where(genre => FilterGenres(genre, filter));

            GatherGamesFromGenres(genres, games, filter.Genres.Any());

            if (!string.IsNullOrEmpty(filter.SearchValue))
                games = games.Where(x => x.Name.ToUpper().StartsWith(filter.SearchValue.ToUpper())).ToList();

            return mapper.Map<IEnumerable<GameDto>>(games);
        }

        private bool FilterGenres(Genre genre, FilterGamesDto filter)
        {
            if (genre.ChildGenres != null)
                if (genre.ChildGenres.Any(ch => filter.Genres.Contains(ch.Name)))
                    return true;
            return filter.Genres.Contains(genre.Name);
        }

        private void GatherGamesFromGenres(IEnumerable<Genre> genres, List<Game> games, bool anyGenresInFilter)
        {
            foreach (var g in genres)
            {
                games.AddRange(g.Games);
                if (g.ChildGenres != null && anyGenresInFilter)
                {
                    foreach (var cg in g.ChildGenres)
                        games.AddRange(cg.Games);
                }
            }
        }
    }
}
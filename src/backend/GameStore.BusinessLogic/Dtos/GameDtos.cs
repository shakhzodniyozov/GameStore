namespace GameStore.BusinessLogic.Dtos
{
    public record GameDto(Guid Id,
                          string Name,
                          string Description,
                          IEnumerable<GenreDto> Genres,
                          string ImagePath,
                          decimal Price);

    public record CreateGameDto(string Name,
                                IEnumerable<Guid> GenreIds,
                                decimal Price,
                                string Description,
                                string ImageAsBase64);

    public record UpdateGameDto(Guid Id,
                                string Name,
                                IEnumerable<Guid> GenreIds,
                                decimal Price,
                                string Description,
                                string? ImageAsBase64);

    public record FilterGamesDto(IEnumerable<string> Genres,
                                 string? SearchValue);
}

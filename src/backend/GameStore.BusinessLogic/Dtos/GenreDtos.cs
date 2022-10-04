namespace GameStore.BusinessLogic.Dtos
{
    public record GenreDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Parent { get; init; }
    }

    public record CreateGenreDto(string Name,
                                 IEnumerable<string> Children);

    public record UpdateGenreDto(Guid Id,
                                 string Name);

    public record GenreWithDetailsDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public IEnumerable<GenreDto>? Children { get; set; }
    }
}

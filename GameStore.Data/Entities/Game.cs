namespace GameStore.Data.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}

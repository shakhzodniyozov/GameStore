namespace GameStore.BusinessLogic.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string? ImagePath { get; set; }
        public decimal Price { get; set; }
    }
}

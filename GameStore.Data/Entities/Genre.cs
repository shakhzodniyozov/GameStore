namespace GameStore.Data.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}

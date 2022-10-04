namespace GameStore.Data.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Game> Games { get; set; } = new List<Game>();
        public Genre? Parent { get; set; }
        public Guid? ParentId { get; set; }
        public ICollection<Genre>? ChildGenres { get; set; }
    }
}

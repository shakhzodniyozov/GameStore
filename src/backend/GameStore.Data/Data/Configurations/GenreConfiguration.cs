using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.Data.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(64);

            builder.HasMany(g => g.Games).WithMany(g => g.Genres);

            builder.HasOne(x => x.Parent)
                   .WithMany(x => x.ChildGenres)
                   .HasForeignKey(x => x.ParentId)
                   .IsRequired(false);
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");
            builder.HasKey(movie => movie.Id);
            builder.Property(movie => movie.Name).HasMaxLength(255);
            builder.Property(movie => movie.ReleasedOn).IsRequired();
        }
    }
}

using BlankSolution.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlankSolution.Data.Configurations;

public class MovieImageConfiguration : IEntityTypeConfiguration<MovieImage>
{
	public void Configure(EntityTypeBuilder<MovieImage> builder)
	{
		builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(100);

		builder.HasOne(x=>x.Movie).WithMany(x=>x.MovieImages).HasForeignKey(x=>x.MovieId).OnDelete(DeleteBehavior.Cascade);
	}
}

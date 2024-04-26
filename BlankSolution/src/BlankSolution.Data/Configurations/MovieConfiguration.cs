using BlankSolution.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlankSolution.Data.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
	public void Configure(EntityTypeBuilder<Movie> builder)
	{
		builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(75);
		builder.Property(x => x.Desc)
				.IsRequired()
				.HasMaxLength(575);
		builder.Property(x => x.CostPrice)
				.IsRequired()
				.HasColumnType("decimal(18,2)");
		builder.Property(x => x.SalePrice)
				.IsRequired()
				.HasColumnType("decimal(18,2)");

		builder.HasOne(x=>x.Genre)
				.WithMany(x=>x.Movies)
				.HasForeignKey(x=>x.GenreId)
				.OnDelete(DeleteBehavior.Cascade);
	}
}

using BlankSolution.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlankSolution.Data.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
	public void Configure(EntityTypeBuilder<Genre> builder)
	{
		builder.Property(x=>x.Name)
				.IsRequired()
				.HasMaxLength(45);
	}
}

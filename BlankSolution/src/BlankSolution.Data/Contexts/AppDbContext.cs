using BlankSolution.Core.Entities;
using BlankSolution.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlankSolution.Data.Contexts;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieImage> MovieImages { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(GenreConfiguration).Assembly);

		base.OnModelCreating(modelBuilder);
	}
}

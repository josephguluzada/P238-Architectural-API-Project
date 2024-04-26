using BlankSolution.Core.Entities;
using BlankSolution.Core.Repositories;
using BlankSolution.Data.Contexts;

namespace BlankSolution.Data.Repositories;

public class MovieRepository : GenericRepository<Movie>, IMovieRepository
{
	public MovieRepository(AppDbContext context) : base(context)
	{
	}
	
}

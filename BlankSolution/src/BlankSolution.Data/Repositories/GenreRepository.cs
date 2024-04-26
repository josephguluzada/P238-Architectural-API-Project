using BlankSolution.Core.Entities;
using BlankSolution.Core.Repositories;
using BlankSolution.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlankSolution.Data.Repositories;

public class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
	public GenreRepository(AppDbContext context) : base(context)
	{
	}

	public async Task<bool> IsExist(Expression<Func<Genre, bool>> expression)
	{

		bool isExistGenre = await Table.AnyAsync(expression);
		return isExistGenre;
	}
}

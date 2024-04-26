using BlankSolution.Core.Entities;
using System.Linq.Expressions;

namespace BlankSolution.Core.Repositories;

public interface IGenreRepository : IGenericRepository<Genre>
{

	Task<bool> IsExist(Expression<Func<Genre,bool>> expression);
}

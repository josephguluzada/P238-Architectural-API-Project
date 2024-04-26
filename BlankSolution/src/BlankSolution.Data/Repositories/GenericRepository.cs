using BlankSolution.Core.Entities;
using BlankSolution.Core.Repositories;
using BlankSolution.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlankSolution.Data.Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
	{
		private readonly AppDbContext _context;

		public GenericRepository(AppDbContext context)
        {
			_context = context;
		}

        public DbSet<TEntity> Table => _context.Set<TEntity>();	//context.Genres

		public async Task<int> CommitAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Delete(TEntity entity)
		{
			Table.Remove(entity);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, params string[] includes)
		{
			var query = Table.AsQueryable();

			if (includes is not null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			return expression is not null
					? await query.Where(expression).ToListAsync()
					: await query.ToListAsync();
		}

		public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null, params string[] includes)
		{
			var query = Table.AsQueryable();

			if (includes is not null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			return expression is not null
					? await query.Where(expression).FirstOrDefaultAsync()
					: await query.FirstOrDefaultAsync();
		}

		public async Task<TEntity> GetByIdAsync(int id)
		{
			return await Table.FindAsync(id);
		}

		public async Task InsertAsync(TEntity entity)
		{
			await Table.AddAsync(entity);
		}
	}
}

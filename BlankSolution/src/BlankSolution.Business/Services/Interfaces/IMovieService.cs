using BlankSolution.Business.DTOs.MovieDTOs;
using BlankSolution.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSolution.Business.Services.Interfaces
{
	public interface IMovieService
	{
		Task<MovieGetDto> GetByIdAsync(int id);
		Task CreateAsync(MovieCreateDto dto);
		Task<IEnumerable<MovieGetDto>> GetAllAsync();
		Task UpdateAsync(int id, MoviePutDto moviePutDto);
		Task DeleteAsync(int id, MovieDeleteDto movieDeleteDto);
	}
}

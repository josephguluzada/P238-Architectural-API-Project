using BlankSolution.Business.DTOs.GenreDtos;
using BlankSolution.Core.Entities;

namespace BlankSolution.Business.Services.Interfaces;

public interface IGenreService
{
	Task<IEnumerable<GenreGetListDto>> GetAllAsync();
	Task<Genre> CreateAsync(GenreCreateDto genre);
	Task<IEnumerable<Genre>> GetAllPaginated(int page = 1, int pageSize=2);
}

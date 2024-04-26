using BlankSolution.Business.DTOs.GenreDtos;
using BlankSolution.Business.Services.Interfaces;
using BlankSolution.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlankSolution.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class GenresController : ControllerBase
{
	private readonly IGenreService _genreService;

	public GenresController(IGenreService genreService)
	{
		_genreService = genreService;
	}

	[HttpGet("")]
	public async Task<IActionResult> GetAllAsync()
	{
		return Ok(await _genreService.GetAllAsync());
	}

	[HttpGet("")]
	public async Task<IActionResult> GetAllPaginated(int page = 1, int pageSize = 2)
	{
		return Ok(await _genreService.GetAllPaginated(page, pageSize));
	}

	[HttpPost("")]
	public async Task<IActionResult> Create(GenreCreateDto genreDto)
	{
		return Ok(await _genreService.CreateAsync(genreDto));
	}
}

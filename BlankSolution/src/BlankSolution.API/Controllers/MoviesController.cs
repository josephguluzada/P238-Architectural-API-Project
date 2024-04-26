using BlankSolution.Business.CustomExceptions.CommonExceptions;
using BlankSolution.Business.DTOs.MovieDTOs;
using BlankSolution.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlankSolution.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		private readonly IMovieService _movieService;

		public MoviesController(IMovieService movieService)
        {
			_movieService = movieService;
		}
		[Authorize(Roles ="Member")]
		[HttpGet("")]
		public async Task<IActionResult> GetAll() 
		{
			return Ok(await _movieService.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			return Ok(await _movieService.GetAllAsync());
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id, MovieDeleteDto dto)
		{
			try
			{
				await _movieService.DeleteAsync(id, dto);
			}
			catch (NotFoundException ex)
			{
				return StatusCode(ex.StatusCode, ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, MoviePutDto moviePutDto)
		{
			try
			{
				await _movieService.UpdateAsync(id, moviePutDto);
			}
			catch(NotFoundException ex)
			{
				return StatusCode(ex.StatusCode, ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok();
		}

		[HttpPost("")]
		public async Task<IActionResult> Create(MovieCreateDto createDto)
		{
			try
			{
				await _movieService.CreateAsync(createDto);
			}
			catch(NotFoundException ex)
			{
				return StatusCode(ex.StatusCode,ex.Message);
			}
			catch (Exception)
			{

				throw;
			}

			return Created();
		}
	}
}

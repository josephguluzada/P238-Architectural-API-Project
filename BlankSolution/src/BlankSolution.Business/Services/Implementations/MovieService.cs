using AutoMapper;
using BlankSolution.Business.CustomExceptions.CommonExceptions;
using BlankSolution.Business.DTOs.MovieDTOs;
using BlankSolution.Business.Helpers;
using BlankSolution.Business.Services.Interfaces;
using BlankSolution.Core.Entities;
using BlankSolution.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BlankSolution.Business.Services.Implementations;

public class MovieService : IMovieService
{
	private readonly IMovieRepository _movieRepository;
	private readonly IGenreRepository _genreRepository;
	private readonly IMapper _mapper;
	private readonly IWebHostEnvironment _env;

	public MovieService(
		IMovieRepository movieRepository,
		IGenreRepository genreRepository,
		IMapper mapper,
		IWebHostEnvironment env)
        {
		_movieRepository = movieRepository;
		_genreRepository = genreRepository;
		_mapper = mapper;
		_env = env;
	}

    public async Task CreateAsync(MovieCreateDto dto)
	{
		if (!await _genreRepository.Table.AnyAsync(x => x.Id == dto.GenreId))
			throw new NotFoundException(404, "Genre not found");

		Movie movie = _mapper.Map<Movie>(dto);
		movie.MovieImages = new List<MovieImage>();
		MovieImage image = new MovieImage
		{
			CreatedDate = DateTime.Now,
			UpdatedDate = DateTime.Now,
			ImageUrl = dto.ImageFile.SaveFile(_env.WebRootPath,"uploads"),
			IsDeleted = false
		};

		movie.MovieImages.Add(image);
		movie.CreatedDate = DateTime.Now;
		movie.UpdatedDate = DateTime.Now;

		await _movieRepository.InsertAsync(movie);
		await _movieRepository.CommitAsync();
	}

	public async Task DeleteAsync(int id, MovieDeleteDto movieDeleteDto)
	{
		if (id != movieDeleteDto.Id) throw new Exception("Id must be valid!");

		Movie movie = await _movieRepository.GetByIdAsync(id);

		if (movie is null) throw new NotFoundException(404, "Movie not var!");

		 _movieRepository.Delete(movie);
		await _movieRepository.CommitAsync();
	}

	public async Task<IEnumerable<MovieGetDto>> GetAllAsync()
	{
		var datas = await _movieRepository.GetAllAsync(null, "Genre","MovieImages");
		IEnumerable<MovieGetDto> dtos = new List<MovieGetDto>(){};
		
		dtos = _mapper.Map<IEnumerable<MovieGetDto>>(datas);

		return dtos;
	}

	public Task<MovieGetDto> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateAsync(int id, MoviePutDto moviePutDto)
	{
		if(id != moviePutDto.Id) throw new Exception("Id must be valid!");

		if(!await _genreRepository.IsExist(x=>x.Id == moviePutDto.GenreId))
			throw new NotFoundException(404, "Genre not found");

		var existData = await _movieRepository.GetByIdAsync(id);

		if (existData is null) throw new NotFoundException(404, "Movie not found!");

		_mapper.Map(moviePutDto,existData);
		
		existData.UpdatedDate = DateTime.UtcNow;

		await _movieRepository.CommitAsync();
	}
}

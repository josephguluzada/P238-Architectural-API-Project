using AutoMapper;
using BlankSolution.Business.DTOs.GenreDtos;
using BlankSolution.Business.DTOs.MovieDTOs;
using BlankSolution.Business.DTOs.MovieImagesDtos;
using BlankSolution.Core.Entities;

namespace BlankSolution.Business.MappingProfiles;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<MoviePutDto, Movie>().ReverseMap();
        CreateMap<MovieGetDto, Movie>().ReverseMap();
        CreateMap<MovieCreateDto, Movie>().ReverseMap();
        CreateMap<MovieImage, MovieImageGetDto>().ReverseMap();
        CreateMap<Genre, GenreGetListDto>().ReverseMap();
        CreateMap<Genre, GenreCreateDto>().ReverseMap();
    }
}

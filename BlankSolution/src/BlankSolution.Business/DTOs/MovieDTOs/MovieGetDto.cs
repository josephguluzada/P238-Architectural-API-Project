using BlankSolution.Business.DTOs.MovieImagesDtos;
using BlankSolution.Core.Entities;

namespace BlankSolution.Business.DTOs.MovieDTOs;

public class MovieGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string GenreName { get; set; }
    public List<MovieImageGetDto> MovieImages { get; set; }
}

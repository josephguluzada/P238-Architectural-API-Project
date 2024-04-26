using BlankSolution.Business.DTOs.MovieDTOs;
using FluentValidation;

namespace BlankSolution.Business.DTOValidators.MovieDtoValidators;

public class MovieDeleteDtoValidator : AbstractValidator<MovieDeleteDto>
{
    public MovieDeleteDtoValidator()
    {
		RuleFor(x => x.Id)
				.NotNull()
				.GreaterThan(0)
				.WithMessage("Id 0dan boyuk olmalidir!");
	}
}

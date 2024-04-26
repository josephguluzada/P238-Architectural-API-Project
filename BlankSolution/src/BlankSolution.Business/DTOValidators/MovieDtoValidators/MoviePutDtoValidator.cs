using BlankSolution.Business.DTOs.MovieDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSolution.Business.DTOValidators.MovieDtoValidators
{
	public class MoviePutDtoValidator : AbstractValidator<MoviePutDto>
	{
        public MoviePutDtoValidator()
        {
			RuleFor(x => x.Id)
				.NotNull()
				.GreaterThan(0)
				.WithMessage("Id 0dan boyuk olmalidir!");
			RuleFor(x => x.Name)
		   .NotEmpty().WithMessage("Bos ola bilmez")
		   .NotNull().WithMessage("Null ola bilmez")
		   .MaximumLength(75).WithMessage("Maximum 75 ola biler")
		   .MinimumLength(1).WithMessage("Min 1 ola biler");

			RuleFor(x => x.Desc)
				.NotEmpty().WithMessage("Bos ola bilmez")
				.NotNull().WithMessage("Null ola bilmez")
				.MaximumLength(575).WithMessage("Maximum 575 ola biler")
				.MinimumLength(20).WithMessage("Min 20 ola biler");

			RuleFor(x => x.CostPrice)
				.NotNull().WithMessage("Null ola bilmez")
				.GreaterThanOrEqualTo(1).When(x => !x.IsDeleted).WithMessage("Active movie'min qiymeti olmalidir");

			RuleFor(x => x.SalePrice)
				.NotNull().WithMessage("Null ola bilmez")
				.GreaterThanOrEqualTo(1).When(x => !x.IsDeleted).WithMessage("Active movie'min qiymeti olmalidir");

			RuleFor(x => x).Custom((x, context) =>
			{
				if (x.SalePrice < x.CostPrice) // 39 40
				{
					context.AddFailure(nameof(x.SalePrice), "SalePrice CostPrice'dan boyuk olmalidir");
				}
			});
		}
    }
}

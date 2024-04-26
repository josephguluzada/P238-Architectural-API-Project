using BlankSolution.Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BlankSolution.Business.DTOs.MovieDTOs;

public class MovieCreateDto
{
    public int GenreId { get; set; }
	public string Name { get; set; }
	public string Desc { get; set; }
	public double CostPrice { get; set; } // 40
	public double SalePrice { get; set; } // 40>
    public bool IsDeleted { get; set; }
    public IFormFile ImageFile { get; set; }
}

public class MovieCreateDtoValidator : AbstractValidator<MovieCreateDto>
{
    public MovieCreateDtoValidator()
    {
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
			.GreaterThanOrEqualTo(1).When(x=>!x.IsDeleted).WithMessage("Active movie'min qiymeti olmalidir");

		RuleFor(x => x.SalePrice)
			.NotNull().WithMessage("Null ola bilmez");

		RuleFor(x => x).Custom((x, context) =>
		{
			if(x.SalePrice < x.CostPrice) // 39 40
			{
				context.AddFailure(nameof(x.SalePrice), "SalePrice CostPrice'dan boyuk olmalidir");
			}
		});

		RuleFor(x => x.ImageFile)
			.NotNull()
			.Must(x => x.ContentType == "image/png" || x.ContentType == "image/jpeg")
			.WithMessage("Image must be jpg/png")
			.Must(x=> x.Length <= 2*1024*1024)
			.WithMessage("Image size must be lower than 2 mb");

	}
}

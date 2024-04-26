using BlankSolution.Business.Services.Implementations;
using BlankSolution.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlankSolution.Business;

public static class ServiceRegistration
{
	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<IGenreService, GenreService>();
		services.AddScoped<IMovieService, MovieService>();
		services.AddScoped<IAuthService, AuthService>();
	}
}

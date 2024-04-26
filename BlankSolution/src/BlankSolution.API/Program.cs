using BlankSolution.Data;
using BlankSolution.Business;
using BlankSolution.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using BlankSolution.Business.DTOs.MovieDTOs;
using BlankSolution.Business.MappingProfiles;
using BlankSolution.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddValidatorsFromAssemblyContaining<MovieCreateDtoValidator>();
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddControllers().AddFluentValidation(opt =>
{
	opt.RegisterValidatorsFromAssembly(typeof(MovieCreateDtoValidator).Assembly);
}).AddNewtonsoftJson(options =>
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
	opt.Password.RequireNonAlphanumeric = true;
	opt.Password.RequiredLength = 8;
	opt.User.RequireUniqueEmail = true;
})
	.AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
	opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
	{
		ValidAudience = builder.Configuration.GetSection("JWT:audience").Value,
		ValidIssuer = builder.Configuration.GetSection("JWT:issuer").Value,

		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:securityKey").Value)),

		ValidateAudience = true,
		ValidateIssuer = true,
		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero,
	};
});

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

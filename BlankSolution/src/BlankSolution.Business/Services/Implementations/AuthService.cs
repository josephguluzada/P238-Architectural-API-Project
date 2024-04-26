using BlankSolution.Business.DTOs.TokenDtos;
using BlankSolution.Business.DTOs.UserDtos;
using BlankSolution.Business.Services.Interfaces;
using BlankSolution.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlankSolution.Business.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    public async Task<TokenResponseDto> Login(UserLoginDto userLoginDto)
    {
        var user = await _userManager.FindByNameAsync(userLoginDto.userName);

        if (user is null)
        {
            throw new Exception("Invalid Credentials");
        }

        var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.password, false, false);
        if (!result.Succeeded)
        {
            throw new Exception("Invalid Credentials");
        }

        IList<string> userRoles = await _userManager.GetRolesAsync(user);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("Fullname", user.FullName),
        };

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var securityKey = _configuration.GetSection("JWT:securityKey").Value;
        var tokenExpireDate = DateTime.UtcNow.AddHours(3);
        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

        SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                audience: _configuration.GetSection("JWT:audience").Value,
                issuer: _configuration.GetSection("JWT:issuer").Value,
                expires: tokenExpireDate
                );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return new TokenResponseDto(user.UserName, token, tokenExpireDate);
    }

    public async Task Register(UserRegisterDto userRegisterDto)
    {
        var user = await _userManager.FindByNameAsync(userRegisterDto.userName);

        if (user is not null)
        {
            throw new Exception("Username already exist!");
        }

        user = await _userManager.FindByEmailAsync(userRegisterDto.email);
        if (user is not null)
        {
            throw new Exception("Email already exist!");
        }

        AppUser newUser = new()
        {
            UserName = userRegisterDto.userName,
            Email = userRegisterDto.email,
            FullName = userRegisterDto.fullName,
        };

        var userResult = await _userManager.CreateAsync(newUser, userRegisterDto.password);

        if (userResult.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, "Member");
        }
    }
}

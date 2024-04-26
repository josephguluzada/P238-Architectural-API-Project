using BlankSolution.Business.DTOs.TokenDtos;
using BlankSolution.Business.DTOs.UserDtos;

namespace BlankSolution.Business.Services.Interfaces;

public interface IAuthService
{
    Task<TokenResponseDto> Login(UserLoginDto userLoginDto);
    Task Register(UserRegisterDto userRegisterDto);
}

using BlankSolution.Business.DTOs.UserDtos;
using BlankSolution.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlankSolution.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(IAuthService authService, RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            _roleManager = roleManager;
        }
        [HttpPost("")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            await _authService.Register(userRegisterDto);
            return Ok();
        }

        [HttpPost("")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            return Ok(await _authService.Login(userLoginDto));
        }

        //[HttpGet("")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    var role1 = new IdentityRole("Member");
        //    var role2 = new IdentityRole("Admin");
        //    var role3 = new IdentityRole("SuperAdmin");

        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);

        //    return Ok();
        //}

    }
}

using LibraryManagement.Application.Dtos.Auth;
using LibraryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            try
            {
                var userDto = await _authService.RegisterAsync(registerDto);
                return Ok(userDto);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var tokenDto = await _authService.LoginAsync(loginDto);
                return Ok(tokenDto);
            }
            catch (ApplicationException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

    }
}

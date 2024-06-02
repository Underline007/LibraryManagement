using LibraryManagement.Application.Dtos.Auth;
using LibraryManagement.Application.Dtos.User;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<TokenDto> LoginAsync(LoginDto loginDto);
        //Task<TokenDto> RefreshTokenAsync();
    }
}

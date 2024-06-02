using LibraryManagement.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUserAsync(int pageNumber, int pageSize);
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task UpdateUserAsync(Guid id, UserCreateEditDto createEditUserDto);
        Task<UserDto> GetUserByUserNameAsync(string username);
        Task<UserDto> GetUserByEmailAsync(string email);
    }
}

using AutoMapper;
using LibraryManagement.Application.Dtos.User;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Interfaces;
using LibraryManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserDto>> GetAllUserAsync(int pageNumber, int pageSize)
        {
            var users = await _userRepository.GetAll(pageNumber, pageSize);
            return _mapper.Map<PaginatedList<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(Guid id, UserCreateEditDto createEditUserDto)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            _mapper.Map(createEditUserDto, user);
            await _userRepository.UpdateAsync(user);
        }

        public async Task<UserDto> GetUserByUserNameAsync(string username)
        {
            var user = await _userRepository.GetByUserNameAsync(username);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}

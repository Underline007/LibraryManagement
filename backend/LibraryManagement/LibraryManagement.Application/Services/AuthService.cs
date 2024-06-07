using AutoMapper;
using CloudinaryDotNet.Actions;
using LibraryManagement.Application.Dtos.Auth;
using LibraryManagement.Application.Dtos.User;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IMapper mapper, IPhotoService photoService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var exsitingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (exsitingUser != null)
            {
                throw new ApplicationException("Email already registered");
            }

            var exsitingUserName = await _userManager.FindByNameAsync(registerDto.Username);
            if (exsitingUserName != null)
            {
                throw new ApplicationException("UserName already registered");

            }


            if (registerDto.Password.Length < 8)
            {
                throw new ApplicationException("Password must be greater than 8 character");
            }
            var avatarUrl = await UploadAvatarAsync(registerDto.Avatar);

            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                Address = registerDto.Address,
                Avatar = avatarUrl,

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException("User registration failed.");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new ApplicationException("Invalid email or password.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Invalid email or password.");
            }

            var token = await GenerateJwtToken(user, _userManager); 

            return new TokenDto
            {
                Email = user.Email,
                Token = token,
                RefreshToken = GenerateRefreshToken()
            };
        }

        private async Task<string> GenerateJwtToken(User user, UserManager<User> userManager)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role.ToString())

    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddYears(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }





        private async Task<string> UploadAvatarAsync(IFormFile avatar)
        {
            if (avatar == null) return null;

            var uploadResult = await _photoService.AddPhotoAsync(avatar);
            if (uploadResult.Error != null)
            {
                throw new ApplicationException("Avatar upload failed.");
            }

            return uploadResult.SecureUrl.AbsoluteUri;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        //public Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

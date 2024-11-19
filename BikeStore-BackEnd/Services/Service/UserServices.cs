using AutoMapper;
using BikeStore_BackEnd.Data;
using BikeStore_BackEnd.Dto;
//using BikeStore_BackEnd.DTO;
using BikeStore_BackEnd.IServices;
using BikeStore_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BikeStore_BackEnd.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly BikeApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(IMapper mapper, BikeApplicationDbContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserDTO> RegisterUser(UserRegisterDTO userRegisterDto)
        {
            var user = _mapper.Map<User>(userRegisterDto);
            user.PasswordHash = userRegisterDto.Password;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<string> LoginUser(UserLoginDTO userLoginDto)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
            //if (user == null || user.PasswordHash != userLoginDto.Password) // Simplified password check
            //{
            //    throw new Exception("Invalid credentials");
            //}

            //var authClaims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim("role", user.Role)
            //};

            //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //var token = new JwtSecurityToken(
            //    issuer: _configuration["Jwt:Issuer"],
            //    audience: _configuration["Jwt:Audience"],
            //    expires: DateTime.Now.AddHours(5),
            //    claims: authClaims,
            //    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            //);

            //return new JwtSecurityTokenHandler().WriteToken(token);

            
                if (userLoginDto == null || string.IsNullOrEmpty(userLoginDto.Email) || string.IsNullOrEmpty(userLoginDto.Password))
                    throw new ArgumentNullException("Email or Password cannot be null.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
                if (user == null ) // Use the actual hashing function
                {
                    throw new Exception("Invalid credentials");
                }
                if(userLoginDto.Password != user.PasswordHash)
                 {
                throw new Exception("Invalid credentials");
            }

            var authClaims = new List<Claim>
                {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role", user.Role)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserDTO> GetUserProfile(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUserProfile(UserDTO userDto)
        {
            var user = await _context.Users.FindAsync(userDto.UserId);
            if (user == null)
            {
                throw new Exception($"User with ID {userDto.UserId} not found.");
            }
            _mapper.Map(userDto, user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public Task LogoutUser(int userId)
        {
            // Handle logout logic here (token invalidation)
            return Task.CompletedTask;
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }
    }
}


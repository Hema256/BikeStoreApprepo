using AutoMapper;
using BikeStore_BackEnd.Dto;
using BikeStore_BackEnd.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BikeStore_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // POST: api/User/Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registeredUser = await _userService.RegisterUser(userRegisterDto);
            return CreatedAtAction(nameof(GetUserProfile), new { id = registeredUser.UserId }, registeredUser);
        }

        // POST: api/User/Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginResult = await _userService.LoginUser(userLoginDto);
            if (string.IsNullOrEmpty(loginResult))
            {
                return Unauthorized("Invalid login attempt.");
            }

            // Return token or session details here
            return Ok(loginResult);
        }

        // GET: api/User/Profile/{id}
        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var userProfile = await _userService.GetUserProfile(id);
            if (userProfile == null)
            {
                return NotFound("User not found.");
            }
            return Ok(userProfile);
        }

        // PUT: api/User/Profile/{id}
        [HttpPut("profile/{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, UserDTO userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            var updatedUser = await _userService.UpdateUserProfile(userDto);
            if (updatedUser == null)
            {
                return NotFound("User not found.");
            }

            return Ok(updatedUser);
        }

        // POST: api/User/Logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(int userId)
        {
            await _userService.LogoutUser(userId);
            return Ok("Logout successful.");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroceryStoreAPI.Data;
using GroceryStoreAPI.DTOs;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services;

namespace GroceryStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly GroceryStoreContext _context;
        private readonly IAuthService _authService;

        public AuthController(GroceryStoreContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                return BadRequest("Email already exists");

            var user = new User
            {
                Email = registerDto.Email,
                Password = _authService.HashPassword(registerDto.Password),
                Name = registerDto.Name,
                Address = registerDto.Address,
                Phone = registerDto.Phone
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _authService.GenerateJwtToken(user);

            return Ok(new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Address = user.Address,
                Phone = user.Phone,
                Token = token
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !_authService.VerifyPassword(loginDto.Password, user.Password))
                return BadRequest("Invalid credentials");

            var token = _authService.GenerateJwtToken(user);

            return Ok(new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Address = user.Address,
                Phone = user.Phone,
                Token = token
            });
        }
    }
}
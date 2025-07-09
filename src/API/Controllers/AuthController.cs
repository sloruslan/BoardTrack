using API.Service;
using Application.DTO.Auth;
using Application.Interfaces.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public AuthController(JwtService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user != null)
            {
                if (_userRepository.CheckPasswordAsync(user, request.Password))
                {
                    var token = _jwtService.GenerateToken(request.Email, user.Role.Name, user.Id);
                    return Ok(token);
                }
            }
            return BadRequest("Invalid username or password");
        }
    }
}


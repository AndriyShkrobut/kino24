using kino24_user.BL.Interfaces;
using kino24_user.BL.DTO.UserAuthentication;
using kino24_user.BL.Interfaces.Auth;
using kino24_user.BL.Interfaces.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kino24_user.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(
            IAuthService authService,
            IJwtService  jwtService)
        {
            _authService = authService;
            _jwtService  = jwtService;
        }

        [HttpGet("health")]
        [AllowAnonymous]
        public async Task<IActionResult> HealthCheck()
        {
            return Ok();
        }

        /// <summary>
        /// Method for registering in system
        /// </summary>
        /// <param name="registerDto">Register model(dto)</param>
        /// <returns>Answer from backend for register method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Problems with registration</response>
        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Incorrect input data");
            }
            var registeredUser = await _authService.FindByEmailAsync(registerDto.Email);
            if (registeredUser != null)
            {
                return BadRequest("User with current email is already registered");
            }
            else
            {
                var result = await _authService.CreateUserAsync(registerDto);
                if (!result.Succeeded)
                {
                    var res = result.ToString();
                    return BadRequest("Registration failed:" + result.ToString());
                }
                else
                {
                    var user = await _authService.FindByEmailAsync(registerDto.Email);
                    var loginResult = await _authService.SignInAsync(new LoginDto { Email=registerDto.Email, Password=registerDto.Password});
                    if (loginResult.Succeeded)
                    {
                        var generatedToken = await _jwtService.GenerateJWTTokenAsync(user);
                        return Ok(new
                        {
                            token = generatedToken,
                            user  = user
                        });
                    }
                    return Ok("Registration succeeded");
                }
            }
        }

        /// <summary>
        /// Method for logining in system
        /// </summary>
        /// <param name="loginDto">Login model(dto)</param>
        /// <returns>Answer from backend for login method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Problems with logining</response>
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return BadRequest("Login failed. Not registered");
                }
                var result = await _authService.SignInAsync(loginDto);
                if (result.Succeeded)
                {
                    var generatedToken = await _jwtService.GenerateJWTTokenAsync(user);
                    return Ok(new { token = generatedToken,
                                    user  = user});
                }
                return BadRequest("Login failed. Incorrect password");
            }
            return BadRequest("Login failed. Invalid input data.");
        }

        [HttpGet("logout")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Logout()
        {
            _authService.SignOutAsync();
            return Ok();
        }
    }
}

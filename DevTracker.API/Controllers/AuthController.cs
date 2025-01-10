using Microsoft.AspNetCore.Mvc;
using DevTracker.Application.Interfaces;
using DevTracker.Application.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;
    private readonly IUserService _userService;
    public AuthController(IUserService userService, JwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _userService.GetUserByUsernameOrEmailAsync(loginRequest.Username);
        if (user == null)
        {
            return Unauthorized("Invalid username or email");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid password");
        }

        var token = _jwtTokenService.GenerateToken(user.Id, user.Username, user.Role.ToString());

        return Ok(new { Token = token });
    }
}


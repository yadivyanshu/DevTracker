using Microsoft.AspNetCore.Mvc;
using DevTracker.Application.Interfaces;

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
        // Find user by username or email from the database
        var user = await _userService.GetUserByUsernameOrEmailAsync(loginRequest.Username);

        // Check if user exists
        if (user == null)
        {
            return Unauthorized("Invalid username or email");
        }

        // Validate password (assuming password hash is stored in the database)
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid password");
        }

        // Generate JWT token
        var token = _jwtTokenService.GenerateToken(user.Id, user.Username, user.Role.ToString());

        // Return the token
        return Ok(new { Token = token });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
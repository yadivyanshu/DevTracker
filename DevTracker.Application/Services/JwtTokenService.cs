using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

public class JwtTokenService
{
    private readonly IConfiguration _configuration; 

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(int userId, string username, string role){
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var key = Encoding.ASCII
        .GetBytes(_configuration.GetSection("JwtSettings").GetSection("Key").Value!);

        List<Claim> claims = 
        [
            new (JwtRegisteredClaimNames.UniqueName, username),
            new (JwtRegisteredClaimNames.Sub, userId.ToString()),
            new (JwtRegisteredClaimNames.Aud,
            _configuration.GetSection("JwtSettings").GetSection("Audience").Value!),
            new (JwtRegisteredClaimNames.Iss,_configuration.GetSection("JwtSettings").GetSection("Issuer").Value!)
        ];

        claims.Add(new Claim(ClaimTypes.Role, role));
        

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            )
        };

        var token  = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
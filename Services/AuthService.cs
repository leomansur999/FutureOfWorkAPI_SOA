using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FutureOfWorkAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FutureOfWorkAPI.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    // Usuários fake em memória (só para o trabalho)
    private readonly List<User> _users = new()
    {
        new User { Username = "admin", Password = "admin123", Role = "Admin" },
        new User { Username = "user", Password = "user123", Role = "User" }
    };

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public User? ValidateUser(string username, string password)
    {
        return _users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
            u.Password == password);
    }

    public string GenerateJwtToken(User user)
    {
        var key = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        // se não encontrar ou for curta, usa uma chave padrão forte
        if (string.IsNullOrWhiteSpace(key) || key.Length < 32)
        {
            key = "GS_SOA_FUTUREOFWORKAPI_SUPER_SECRET_KEY_2025";
        }


        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

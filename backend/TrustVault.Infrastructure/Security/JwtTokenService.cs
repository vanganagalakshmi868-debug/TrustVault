using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TrustVault.Domain.Entities;
namespace TrustVault.Infrastructure.Security;
public class JwtTokenService
{
    private readonly string _key;
    private readonly string _issuer;
    public JwtTokenService(string key, string issuer) { _key = key; _issuer = issuer; }
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_issuer, _issuer, claims, expires: DateTime.UtcNow.AddHours(12), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
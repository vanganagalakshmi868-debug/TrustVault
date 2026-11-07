using Microsoft.AspNetCore.Mvc;
using TrustVault.Api.DTOs;
using TrustVault.Application.Interfaces;
using TrustVault.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
namespace TrustVault.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    private readonly IConfiguration _config;
    public AuthController(IAuthService auth, IConfiguration config) { _auth = auth; _config = config; }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        var user = await _auth.RegisterAsync(req.FullName, req.Email, req.Password);
        if (user == null) return BadRequest("Email already exists");
        return Ok(new { user.Id, user.Email, user.FullName });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var user = await _auth.LoginAsync(req.Email, req.Password);
        if (user == null) return Unauthorized("Invalid credentials");
        var jwt = new JwtTokenService(_config["Jwt:Key"]!, _config["Jwt:Issuer"]!);
        var token = jwt.GenerateToken(user);
        return Ok(new { token, user.Id, user.Email, user.FullName, Role = user.Role.ToString() });
    }
}
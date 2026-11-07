using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrustVault.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace TrustVault.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public UsersController(ApplicationDbContext db) => _db = db;
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _db.Users.Select(u => new { u.Id, u.FullName, u.Email, Role = u.Role.ToString(), u.CreatedAt }).ToListAsync();
        return Ok(users);
    }
}
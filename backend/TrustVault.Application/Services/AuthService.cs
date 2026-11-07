using TrustVault.Application.Interfaces;
using TrustVault.Domain.Entities;
using TrustVault.Infrastructure.Data;
using TrustVault.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
namespace TrustVault.Application.Services;
public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _db;
    public AuthService(ApplicationDbContext db) => _db = db;
    public async Task<User?> RegisterAsync(string fullName, string email, string password)
    {
        if (await _db.Users.AnyAsync(u => u.Email == email)) return null;
        var user = new User { FullName = fullName, Email = email, PasswordHash = PasswordHasher.Hash(password) };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;
        if (!PasswordHasher.Verify(password, user.PasswordHash)) return null;
        return user;
    }
}
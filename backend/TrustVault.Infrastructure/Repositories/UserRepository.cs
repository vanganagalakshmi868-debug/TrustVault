using Microsoft.EntityFrameworkCore;
using TrustVault.Domain.Entities;
using TrustVault.Infrastructure.Data;
namespace TrustVault.Infrastructure.Repositories;
public class UserRepository
{
    private readonly ApplicationDbContext _db;
    public UserRepository(ApplicationDbContext db) => _db = db;
    public Task<User?> GetByEmailAsync(string email) => _db.Users.FirstOrDefaultAsync(u => u.Email == email);
    public Task<List<User>> GetAllAsync() => _db.Users.ToListAsync();
}
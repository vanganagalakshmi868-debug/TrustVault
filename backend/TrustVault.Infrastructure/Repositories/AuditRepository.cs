using Microsoft.EntityFrameworkCore;
using TrustVault.Domain.Entities;
using TrustVault.Infrastructure.Data;
namespace TrustVault.Infrastructure.Repositories;
public class AuditRepository
{
    private readonly ApplicationDbContext _db;
    public AuditRepository(ApplicationDbContext db) => _db = db;
    public Task<List<AuditLog>> GetAllAsync() => _db.AuditLogs.OrderByDescending(a => a.CreatedAt).ToListAsync();
}
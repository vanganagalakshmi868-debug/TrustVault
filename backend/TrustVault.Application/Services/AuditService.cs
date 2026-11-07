using TrustVault.Application.Interfaces;
using TrustVault.Domain.Entities;
using TrustVault.Domain.Enums;
using TrustVault.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace TrustVault.Application.Services;
public class AuditService : IAuditService
{
    private readonly ApplicationDbContext _db;
    public AuditService(ApplicationDbContext db) => _db = db;
    public async Task LogAsync(Guid? userId, Guid? documentId, AuditAction action, string details)
    {
        _db.AuditLogs.Add(new AuditLog { UserId = userId, DocumentId = documentId, Action = action, Details = details });
        await _db.SaveChangesAsync();
    }
    public async Task<List<AuditLog>> GetAllAsync()
        => await _db.AuditLogs.OrderByDescending(a => a.CreatedAt).ToListAsync();
}
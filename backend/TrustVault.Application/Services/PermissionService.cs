using TrustVault.Application.Interfaces;
using TrustVault.Domain.Entities;
using TrustVault.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace TrustVault.Application.Services;
public class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _db;
    public PermissionService(ApplicationDbContext db) => _db = db;
    public async Task<bool> ShareAsync(Guid documentId, Guid ownerId, Guid sharedWithUserId)
    {
        var doc = await _db.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.OwnerId == ownerId);
        if (doc == null) return false;
        if (await _db.DocumentPermissions.AnyAsync(p => p.DocumentId == documentId && p.SharedWithUserId == sharedWithUserId)) return true;
        _db.DocumentPermissions.Add(new DocumentPermission { DocumentId = documentId, SharedWithUserId = sharedWithUserId });
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task<bool> RevokeAsync(Guid documentId, Guid ownerId, Guid sharedWithUserId)
    {
        var perm = await _db.DocumentPermissions.FirstOrDefaultAsync(p => p.DocumentId == documentId && p.SharedWithUserId == sharedWithUserId);
        if (perm == null) return false;
        var doc = await _db.Documents.FindAsync(documentId);
        if (doc == null || doc.OwnerId != ownerId) return false;
        _db.DocumentPermissions.Remove(perm);
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task<bool> CanAccessAsync(Guid documentId, Guid userId)
        => await _db.Documents.AnyAsync(d => d.Id == documentId && (d.OwnerId == userId || d.Permissions.Any(p => p.SharedWithUserId == userId)));
}
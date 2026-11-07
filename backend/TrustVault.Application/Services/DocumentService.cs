using TrustVault.Application.Interfaces;
using TrustVault.Domain.Entities;
using TrustVault.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace TrustVault.Application.Services;
public class DocumentService : IDocumentService
{
    private readonly ApplicationDbContext _db;
    public DocumentService(ApplicationDbContext db) => _db = db;
    public async Task<Document> UploadAsync(Guid ownerId, string fileName, string storedFileName, string contentType, long fileSize)
    {
        var doc = new Document { OwnerId = ownerId, FileName = fileName, StoredFileName = storedFileName, ContentType = contentType, FileSize = fileSize };
        _db.Documents.Add(doc);
        await _db.SaveChangesAsync();
        return doc;
    }
    public async Task<List<Document>> GetMyDocumentsAsync(Guid ownerId)
        => await _db.Documents.Where(d => d.OwnerId == ownerId).ToListAsync();
    public async Task<Document?> GetByIdAsync(Guid id, Guid userId)
        => await _db.Documents.Include(d => d.Permissions).FirstOrDefaultAsync(d => d.Id == id && (d.OwnerId == userId || d.Permissions.Any(p => p.SharedWithUserId == userId)));
    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var doc = await _db.Documents.FirstOrDefaultAsync(d => d.Id == id && d.OwnerId == userId);
        if (doc == null) return false;
        _db.Documents.Remove(doc);
        await _db.SaveChangesAsync();
        return true;
    }
}
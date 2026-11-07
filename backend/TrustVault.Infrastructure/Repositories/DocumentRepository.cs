using Microsoft.EntityFrameworkCore;
using TrustVault.Domain.Entities;
using TrustVault.Infrastructure.Data;
namespace TrustVault.Infrastructure.Repositories;
public class DocumentRepository
{
    private readonly ApplicationDbContext _db;
    public DocumentRepository(ApplicationDbContext db) => _db = db;
    public Task<List<Document>> GetByOwnerAsync(Guid ownerId) => _db.Documents.Where(d => d.OwnerId == ownerId).ToListAsync();
    public Task<Document?> GetByIdAsync(Guid id) => _db.Documents.FirstOrDefaultAsync(d => d.Id == id);
}
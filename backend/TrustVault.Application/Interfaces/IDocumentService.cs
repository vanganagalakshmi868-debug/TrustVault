using TrustVault.Domain.Entities;
namespace TrustVault.Application.Interfaces;
public interface IDocumentService
{
    Task<Document> UploadAsync(Guid ownerId, string fileName, string storedFileName, string contentType, long fileSize);
    Task<List<Document>> GetMyDocumentsAsync(Guid ownerId);
    Task<Document?> GetByIdAsync(Guid id, Guid userId);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}
using TrustVault.Domain.Entities;
namespace TrustVault.Application.Interfaces;
public interface IAuditService
{
    Task LogAsync(Guid? userId, Guid? documentId, Domain.Enums.AuditAction action, string details);
    Task<List<AuditLog>> GetAllAsync();
}
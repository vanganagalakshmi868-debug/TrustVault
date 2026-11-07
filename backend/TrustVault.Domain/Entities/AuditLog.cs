using TrustVault.Domain.Enums;
namespace TrustVault.Domain.Entities;
public class AuditLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? UserId { get; set; }
    public Guid? DocumentId { get; set; }
    public AuditAction Action { get; set; }
    public string Details { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
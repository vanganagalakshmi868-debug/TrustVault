namespace TrustVault.Domain.Entities;
public class DocumentPermission
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid DocumentId { get; set; }
    public Guid SharedWithUserId { get; set; }
    public bool CanDownload { get; set; } = true;
    public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
    public Document? Document { get; set; }
}
namespace TrustVault.Domain.Entities;
public class Document
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OwnerId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public User? Owner { get; set; }
    public ICollection<DocumentPermission> Permissions { get; set; } = new List<DocumentPermission>();
}
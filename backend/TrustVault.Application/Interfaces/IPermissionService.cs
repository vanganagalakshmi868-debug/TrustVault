namespace TrustVault.Application.Interfaces;
public interface IPermissionService
{
    Task<bool> ShareAsync(Guid documentId, Guid ownerId, Guid sharedWithUserId);
    Task<bool> RevokeAsync(Guid documentId, Guid ownerId, Guid sharedWithUserId);
    Task<bool> CanAccessAsync(Guid documentId, Guid userId);
}
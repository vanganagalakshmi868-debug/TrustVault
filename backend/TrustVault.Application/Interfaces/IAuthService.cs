using TrustVault.Domain.Entities;
namespace TrustVault.Application.Interfaces;
public interface IAuthService
{
    Task<User?> RegisterAsync(string fullName, string email, string password);
    Task<User?> LoginAsync(string email, string password);
}
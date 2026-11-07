using TrustVault.Domain.Entities;
using TrustVault.Domain.Enums;
using TrustVault.Infrastructure.Security;
namespace TrustVault.Infrastructure.Data;
public static class DbInitializer
{
    public static void Seed(ApplicationDbContext db)
    {
        db.Database.EnsureCreated();
        if (!db.Users.Any())
        {
            db.Users.Add(new User
            {
                FullName = "Admin User",
                Email = "admin@trustvault.com",
                PasswordHash = PasswordHasher.Hash("Admin@123"),
                Role = UserRole.Admin
            });
            db.SaveChanges();
        }
    }
}
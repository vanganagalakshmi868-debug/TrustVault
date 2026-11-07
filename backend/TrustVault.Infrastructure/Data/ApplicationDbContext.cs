using Microsoft.EntityFrameworkCore;
using TrustVault.Domain.Entities;
namespace TrustVault.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<DocumentPermission> DocumentPermissions => Set<DocumentPermission>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasMany(u => u.Documents).WithOne(d => d.Owner).HasForeignKey(d => d.OwnerId);
    }
}
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.Infrastructure.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRoles> UserRoles { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    
    public DbSet<Settings> Settings { get; set; }
    
    public DbSet<MigrationLog> MigrationLog { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
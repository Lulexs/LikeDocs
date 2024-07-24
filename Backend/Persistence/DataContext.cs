using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : IdentityDbContext<AppUser> {
    public DataContext(DbContextOptions options) : base(options) {

    }

    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<UserContext> UserContexts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Workspace>()
                .HasMany(w => w.Documents)
                .WithOne(d => d.Workspace)
                .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Document>()
                .HasMany(d => d.UserContexts)
                .WithOne(u => u.Document)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
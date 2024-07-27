using System.Security.Cryptography.X509Certificates;
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
    public DbSet<DiffWrapper> DiffWrapers { get; set; }
    public DbSet<Edit> Edits { get; set; }

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

        builder.Entity<Workspace>()
               .HasMany(w => w.Members)
               .WithMany(m => m.MemberWorkspaces);
        
        builder.Entity<Workspace>()
               .HasOne(x => x.Owner)
               .WithMany(x => x.OwnWorkspaces);

        // builder.Entity<UserContext>()
        //        .HasMany(x => x.Edits)
        //        .WithOne(x => x.UserContext)
        //        .OnDelete(DeleteBehavior.Cascade);
        
        // builder.Entity<Edit>()
        //        .HasMany(x => x.diff)
        //        .WithOne(x => x.Edit)
        //        .OnDelete(DeleteBehavior.Cascade);
    }
}
using DataAccess.Interfaces;

namespace DataAccess;

using Models;
using Microsoft.EntityFrameworkCore;

public sealed class MainContext: DbContext, IMainContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<File> Files { get; set; }

    public MainContext(DbContextOptions<MainContext> options): base(options)
    {
        this.Database.EnsureCreated();
        this.Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Surname).IsRequired();
            entity.Property(e => e.Password).IsRequired();
        });

        modelBuilder.Entity<Models.File>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Size).IsRequired();
        });
        
    }
}

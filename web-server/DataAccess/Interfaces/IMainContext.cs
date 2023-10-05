namespace DataAccess.Interfaces;

using Models;
using Microsoft.EntityFrameworkCore;

public interface IMainContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Models.File> Files { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); 
}
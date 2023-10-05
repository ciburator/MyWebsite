using DataAccess.Interfaces;
using DataAccess.Models;
using Home_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Home_API.Services;

public class UserService: IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMainContext _dbContext;

    public UserService(
        ILogger<UserService> logger,
        IMainContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetAllUsers(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _dbContext.Users.ToListAsync(cancellationToken);
    }

    public async Task<EntityState> AddUser(User user,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await _dbContext.Users.AddAsync(user, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return result.State;
    }

    public async Task<User?> GetUser(int userId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _dbContext.Users.FirstOrDefaultAsync((item) => item.Id == userId, cancellationToken);
    }
}

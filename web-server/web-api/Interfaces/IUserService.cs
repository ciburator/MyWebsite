using Microsoft.EntityFrameworkCore;

namespace Home_API.Interfaces;

using DataAccess.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IUserService
{
    Task<List<User>> GetAllUsers(CancellationToken cancellationToken = default);

    Task<EntityState> AddUser(User user,
        CancellationToken cancellationToken = default);

    Task<User?> GetUser(int userId, CancellationToken cancellationToken = default);
}
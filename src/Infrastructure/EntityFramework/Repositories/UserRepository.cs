using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{

    public async Task<User?> GetByEmailAsync(string email)
    {
        var userData = await context.Users
            .Include(x => x.UserRoles!)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == email);
        return userData;
    }

    public async Task<List<string>> GetUserRoleByEmailAsync(string email)
    {
        var userRoles = await context.Users
            .Where(u => u.Email == email)
            .SelectMany(u => u.UserRoles!)
            .Select(x => x.Role!.Name)
            .ToListAsync();
        if (userRoles == null)
            return [];
        return userRoles;
    }

    public async Task<bool> ExistsAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Email == email) is not null ? true : false;
    }
}

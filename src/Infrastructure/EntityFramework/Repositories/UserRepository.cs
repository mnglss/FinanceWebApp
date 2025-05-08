using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

public class UserRepository(AppDbContext _contex) : Repository<User>(_contex), IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _contex.Users
            .Include(x => x.UserRoles!)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<List<string>> GetUserRoleByEmailAsync(string email)
    {
        var userRoles = await _contex.Users
            .Where(u => u.Email == email)
            .SelectMany(u => u.UserRoles!)
            .Select(x => x.Role!.Name)
            .ToListAsync();
        if (userRoles == null)
            return [];
        return userRoles;
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await _contex.Users.FirstOrDefaultAsync(x => x.Email == email) is not null ? true : false;
    }
}

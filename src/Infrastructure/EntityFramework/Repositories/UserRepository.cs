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
        var user = await _contex.Users
            .Include(x => x.UserRoles!)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
            return [];
        return user.UserRoles!.Select(x => x.Role!.Name).ToList();
    }

    public Task<bool> UserExistsAsync(string email)
    {
        throw new NotImplementedException();
    }
}

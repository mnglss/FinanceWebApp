using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class UserRoleRepository(AppDbContext appDbContext) : IUserRoleRepository
    {
        public async Task<bool> AddAsync(int userId, int roleId)
        {
            try
            {
                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = roleId
                };
                await appDbContext.UserRoles.AddAsync(userRole);
                return await appDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }

        public async Task<IReadOnlyList<string>> GetRolesAsync(int userId)
        {
            try
            {
                var roles = await appDbContext.UserRoles
                    .Where(ur => ur.UserId == userId)
                    .Select(ur => ur.Role.Name)
                    .ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }

        public async Task<bool> HasRoleAsync(int userId, int roleId)
        {
            try
            {
                return await appDbContext.UserRoles
                    .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }

        public async Task<bool> RemoveAsync(int userId, int roleId)
        {
            try
            {
                var userRole = await appDbContext.UserRoles
                    .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
                if (userRole is null)
                {
                    return false;
                }
                appDbContext.UserRoles.Remove(userRole);
                return await appDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }
    }
}

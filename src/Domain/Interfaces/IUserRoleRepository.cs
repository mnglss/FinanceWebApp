namespace Domain.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IReadOnlyList<string>> GetRolesAsync(int userId);
        Task<bool> HasRoleAsync(int userId, int roleId);
        Task<bool> AddAsync(int userId, int roleId);
        Task<bool> RemoveAsync(int userId, int roleId);
    }
}

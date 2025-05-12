using Application.Common.Results;
using Application.DTOs;
using Application.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<PagedResult<UserDto>>?> GetAllAsync(string? search, int pageNumber = 1, int pageSize = 10);
        Task<Result<string>> UpdateAsync(UserUpdateRequest userRequest);
        Task<Result<string>> DeleteAsync(int id);
        Task<Result<UserDto>> GetByIdAsync(int id);
        Task<Result<string>> AssignRoleAsync(AssignRoleRequest roleRequest);
        Task<Result<string>> RemoveRoleAsync(RemoveRoleRequest roleRequest);
        Task<Result<UserDto>> GetByEmailAsync(string email);
    }
}

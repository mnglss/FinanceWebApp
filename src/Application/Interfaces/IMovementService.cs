using Application.Common.Results;
using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMovementService
    {
        Task<Result<string>> CreateAsync(MovementRequest movementCreateRequest);
        Task<Result<List<Movement>>?> GetByUserIdAsync(MovementByUserIdRequest movementByUserId);
        Task<Result<List<string>>> GetCategoryColorAsync();
    }
}

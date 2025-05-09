using Application.Common.Results;
using Application.Models;

namespace Application.Interfaces
{
    public interface IMovementService
    {
        Task<Result<string>> CreateAsync(MovementRequest movementCreateRequest);
    }
}

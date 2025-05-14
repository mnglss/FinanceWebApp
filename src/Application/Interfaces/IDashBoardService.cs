using Application.Common.Results;
using Application.Models;

namespace Application.Interfaces
{
    public interface IDashBoardService
    {
        Task<Result<DashBoard>> GetDashBoardDataAsync(MovementByUserIdRequest request);
    }
}

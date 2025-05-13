using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMovementRepository : IRepository<Movement>
    {
        Task<List<Movement>?> GetByUserIdAsync(int userId, int[] year, int[] month);
    }
}

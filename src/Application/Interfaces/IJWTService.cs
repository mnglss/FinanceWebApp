using Domain.Entities;

namespace Application.Interfaces
{
    public interface IJWTService
    {
        Task<string> GenerateToken(User user);
    }
}

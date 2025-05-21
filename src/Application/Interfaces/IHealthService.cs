
namespace Application.Interfaces
{
    public interface IHealthService
    {
        Task<string> GetLastCheckAsync();
        Task UpdateAsync();
        Task DeleteAsync();
        Task<string> AddAsync();
    }
}

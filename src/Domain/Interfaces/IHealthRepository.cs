namespace Domain.Interfaces
{
    public interface IHealthRepository
    {
        Task AddAsync(DateOnly date);
        Task DeleteAsync();
        Task<DateOnly> GetLastUpdateAsync();
        Task UpdateAsync();
    }
}

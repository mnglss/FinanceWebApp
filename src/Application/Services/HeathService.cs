using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services
{
    public class HeathService(IHealthRepository healthRepository, IUnitOfWork unitOfWork) : IHealthService
    {
        public async Task<string> AddAsync()
        {
            var date = DateOnly.FromDateTime(DateTime.Now);
            await healthRepository.AddAsync(date);
            await unitOfWork.CommitAsync();
            return date.ToShortDateString();
        }

        public async Task DeleteAsync()
        {
            await healthRepository.DeleteAsync();
            await unitOfWork.CommitAsync();
        }

        public async Task<string> GetLastCheckAsync()
        {
            return (await healthRepository.GetLastUpdateAsync()).ToShortDateString();
        }

        public async Task RunHealthJob()
        {
            await GetLastCheckAsync();
            await UpdateAsync();
            await DeleteAsync();
            await AddAsync();
        }

        public async Task UpdateAsync()
        {
            await healthRepository.UpdateAsync();
            await unitOfWork.CommitAsync();
        }
    }
}

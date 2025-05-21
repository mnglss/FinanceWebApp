using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.Repositories
{
    public class HealthRepository(AppDbContext context) : Repository<Audit>(context), IHealthRepository
    {
        public async Task AddAsync(DateOnly date)
        {            
            var audit = new Audit { Id=0, Date = date };
            await AddAsync(audit);
        }

        public async Task DeleteAsync()
        {
            var audit = (await GetAllAsync()).First();
            Delete(audit);
        }

        public async Task<DateOnly> GetLastUpdateAsync()
        {
            var audit = (await GetAllAsync()).First();
            return (await GetAllAsync()).First().Date;
        }

        public async Task UpdateAsync()
        {
            var audit = (await GetAllAsync()).First();
            audit.Date = DateOnly.FromDateTime(DateTime.Now);
            Update(audit);
        }
    }
}

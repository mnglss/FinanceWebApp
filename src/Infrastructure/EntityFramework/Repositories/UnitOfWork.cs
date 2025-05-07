namespace Infrastructure.EntityFramework.Repositories
{
    using System.Threading.Tasks;
    using Domain.Interfaces;
    using Infrastructure.EntityFramework.Context;

    public class UnitOfWork(AppDbContext _appContext) : IUnitOfWork
    {

        public void Commit()
        {
            _appContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _appContext.SaveChangesAsync();
        }
    }
}
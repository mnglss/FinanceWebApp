using Domain.Interfaces;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{

    public class Repository<TEntity>(AppDbContext _appContext) : IRepository<TEntity> where TEntity : class
    {
        internal readonly DbSet<TEntity> _dbSet = _appContext.Set<TEntity>();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var add = await _dbSet.AddAsync(entity);
            return add.Entity;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public TEntity Update(TEntity entity)
        {
            var update = _dbSet.Update(entity);
            return update.Entity;
        }
    }
}
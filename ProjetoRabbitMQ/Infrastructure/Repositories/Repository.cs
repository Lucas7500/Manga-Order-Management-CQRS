using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace ProjetoRabbitMQ.Infrastructure.Repositories
{
    public sealed class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default)
        {
            var query = predicate != null
                ? _dbSet.Where(predicate)
                : _dbSet;

            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetAsync(object id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync([id], ct);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        {
            var query = predicate != null
                ? _dbSet.Where(predicate)
                : _dbSet;

            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task AddAsync(TEntity entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace ProjetoRabbitMQ.Infrastructure.Repositories
{
    public sealed class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
        private readonly HashSet<Expression<Func<TEntity, object>>> _includes = [];

        public DbSet<TEntity> DbSet => _dbSet;

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            var query = predicate != null ? _dbSet.Where(predicate) : _dbSet;
            DefineIncludesForQuery(ref query);
            return query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetAsync(object[] id, CancellationToken cancellationToken = default)
        {
            if (_includes.Count != 0)
            {
                _includes.Clear();
                throw new NotSupportedException("Includes are not supported for GetAsync with id.");
            }

            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.Where(predicate);
            DefineIncludesForQuery(ref query);
            return query.FirstOrDefaultAsync(cancellationToken);
        }

        public Task<bool> HasAnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _dbSet.AnyAsync(predicate, cancellationToken);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
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

        public IQueryRepository<TEntity> IncludeForNextQuery(params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var include in includes)
            {
                _includes.Add(include);
            }

            return this;
        }

        private void DefineIncludesForQuery(ref IQueryable<TEntity> query)
        {
            foreach (var include in _includes)
            {
                query = query.Include(include);
            }

            _includes.Clear();
        }
    }
}

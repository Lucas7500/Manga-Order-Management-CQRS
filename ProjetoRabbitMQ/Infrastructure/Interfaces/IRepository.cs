using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace ProjetoRabbitMQ.Infrastructure.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default);
        Task<TEntity?> GetAsync(TKey id, CancellationToken ct = default);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}

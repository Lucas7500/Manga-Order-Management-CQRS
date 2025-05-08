using System.Linq.Expressions;

namespace ProjetoRabbitMQ.Infrastructure.Interfaces
{
    public interface IQueryRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(object[] id, CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> HasAnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}

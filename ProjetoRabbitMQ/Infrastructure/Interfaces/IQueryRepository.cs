using System.Linq.Expressions;

namespace ProjetoRabbitMQ.Infrastructure.Interfaces
{
    public interface IQueryRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default);
        Task<TEntity?> GetAsync(object[] id, CancellationToken ct = default);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
    }
}

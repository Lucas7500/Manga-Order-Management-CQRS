using Microsoft.EntityFrameworkCore.Storage;

namespace ProjetoRabbitMQ.Infrastructure.Interfaces
{
    public interface ICommandRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}

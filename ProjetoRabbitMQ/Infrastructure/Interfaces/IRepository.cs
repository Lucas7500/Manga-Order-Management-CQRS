using System.Linq.Expressions;

namespace ProjetoRabbitMQ.Infrastructure.Interfaces
{
    public interface IRepository<TEntity> : IQueryRepository<TEntity>, ICommandRepository<TEntity> where TEntity : class
    {
        IQueryRepository<TEntity> IncludeForNextQuery(params Expression<Func<TEntity, object>>[] includes);
    }
}

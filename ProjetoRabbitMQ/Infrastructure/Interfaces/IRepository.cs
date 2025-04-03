using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ProjetoRabbitMQ.Infrastructure.Interfaces
{
    public interface IRepository<TEntity> : IQueryRepository<TEntity>, ICommandRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> DbSet { get; }
        IQueryRepository<TEntity> IncludeForNextQuery(params Expression<Func<TEntity, object>>[] includes);
    }
}

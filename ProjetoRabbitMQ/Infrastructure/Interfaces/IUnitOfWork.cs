using ProjetoRabbitMQ.Models.Manga;
using ProjetoRabbitMQ.Models.MangaOrder;
using ProjetoRabbitMQ.Models.User;

namespace ProjetoRabbitMQ.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Manga> MangaRepository { get; }
        IRepository<MangaOrder> MangaOrderRepository { get; }

        Task CommitAsync();
    }
}

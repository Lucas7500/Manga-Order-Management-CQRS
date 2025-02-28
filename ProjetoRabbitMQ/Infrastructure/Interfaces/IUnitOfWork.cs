using ProjetoRabbitMQ.Models.Manga;
using ProjetoRabbitMQ.Models.MangaOrder;
using ProjetoRabbitMQ.Models.User;

namespace ProjetoRabbitMQ.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User, int> UserRepository { get; }
        IRepository<Manga, Guid> MangaRepository { get; }
        IRepository<MangaOrder, Ulid> MangaOrderRepository { get; }

        Task CommitAsync();
    }
}

using ProjetoRabbitMQ.Models.Manga;
using ProjetoRabbitMQ.Models.MangaOrder;
using ProjetoRabbitMQ.Models.User;

namespace ProjetoRabbitMQ.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<UserEntity> UserRepository { get; }
        IRepository<MangaEntity> MangaRepository { get; }
        IRepository<MangaOrderEntity> MangaOrderRepository { get; }
        IRepository<MangaOrderItemEntity> MangaOrderItemRepository { get;  }

        Task CommitAsync();
    }
}

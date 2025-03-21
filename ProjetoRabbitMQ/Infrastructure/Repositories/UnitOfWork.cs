using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Manga;
using ProjetoRabbitMQ.Models.MangaOrder;
using ProjetoRabbitMQ.Models.User;

namespace ProjetoRabbitMQ.Infrastructure.Repositories
{
    public class UnitOfWork(MySqlContext context) : IUnitOfWork, IDisposable
    {
        private readonly Lazy<IRepository<UserEntity>> _userRepository 
            = new(() => new Repository<UserEntity>(context));
        
        private readonly Lazy<IRepository<MangaEntity>> _mangaRepository 
            = new(() => new Repository<MangaEntity>(context));
        
        private readonly Lazy<IRepository<MangaOrderEntity>> _mangaOrderRepository 
            = new(() => new Repository<MangaOrderEntity>(context));
        
        private readonly Lazy<IRepository<MangaOrderItemEntity>> _mangaOrderItemRepository 
            = new(() => new Repository<MangaOrderItemEntity>(context));

        public IRepository<UserEntity> UserRepository => _userRepository.Value;
        public IRepository<MangaEntity> MangaRepository => _mangaRepository.Value;
        public IRepository<MangaOrderEntity> MangaOrderRepository => _mangaOrderRepository.Value;
        public IRepository<MangaOrderItemEntity> MangaOrderItemRepository => _mangaOrderItemRepository.Value;

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

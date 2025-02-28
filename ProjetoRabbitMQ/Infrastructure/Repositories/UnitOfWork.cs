using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Manga;
using ProjetoRabbitMQ.Models.MangaOrder;
using ProjetoRabbitMQ.Models.User;

namespace ProjetoRabbitMQ.Infrastructure.Repositories
{
    public class UnitOfWork(MySqlContext context) : IUnitOfWork, IDisposable
    {
        private readonly Lazy<IRepository<User>> _userRepository = new(() => new Repository<User>(context));
        private readonly Lazy<IRepository<Manga>> _mangaRepository = new(() => new Repository<Manga>(context));
        private readonly Lazy<IRepository<MangaOrder>> _mangaOrderRepository = new(() => new Repository<MangaOrder>(context));

        public IRepository<User> UserRepository => _userRepository.Value;
        public IRepository<Manga> MangaRepository => _mangaRepository.Value;
        public IRepository<MangaOrder> MangaOrderRepository => _mangaOrderRepository.Value;

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

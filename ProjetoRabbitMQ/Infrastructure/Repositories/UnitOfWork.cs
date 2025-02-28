using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Manga;
using ProjetoRabbitMQ.Models.MangaOrder;
using ProjetoRabbitMQ.Models.User;

namespace ProjetoRabbitMQ.Infrastructure.Repositories
{
    public class UnitOfWork(MySqlContext context) : IUnitOfWork, IDisposable
    {
        private readonly Lazy<IRepository<User, int>> _userRepository = new(() => new Repository<User, int>(context));
        private readonly Lazy<IRepository<Manga, Guid>> _mangaRepository = new(() => new Repository<Manga, Guid>(context));
        private readonly Lazy<IRepository<MangaOrder, Ulid>> _mangaOrderRepository = new(() => new Repository<MangaOrder, Ulid>(context));

        public IRepository<User, int> UserRepository => _userRepository.Value;
        public IRepository<Manga, Guid> MangaRepository => _mangaRepository.Value;
        public IRepository<MangaOrder, Ulid> MangaOrderRepository => _mangaOrderRepository.Value;

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

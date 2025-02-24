using Microsoft.EntityFrameworkCore;
using ProjetoRabbitMQ.Infrastructure.Configuration;
using ProjetoRabbitMQ.Models;

namespace ProjetoRabbitMQ.Infrastructure
{
    public sealed class MySqlContext(DbContextOptions<MySqlContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Manga> Mangas { get; set; } = null!;
        public DbSet<MangaOrder> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(DbConfig.ConfigureUserEntity);
            modelBuilder.Entity<Manga>(DbConfig.ConfigureMangaEntity);
            modelBuilder.Entity<MangaOrder>(DbConfig.ConfigureMangaOrderEntity);
        }
    }
}

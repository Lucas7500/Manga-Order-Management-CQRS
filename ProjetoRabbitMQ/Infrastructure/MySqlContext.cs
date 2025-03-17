using Microsoft.EntityFrameworkCore;
using ProjetoRabbitMQ.Infrastructure.Configuration;
using ProjetoRabbitMQ.Models.Joins;
using ProjetoRabbitMQ.Models.Manga;
using ProjetoRabbitMQ.Models.MangaOrder;
using ProjetoRabbitMQ.Models.User;

namespace ProjetoRabbitMQ.Infrastructure
{
    public sealed class MySqlContext(DbContextOptions<MySqlContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<MangaEntity> Mangas { get; set; } = null!;
        public DbSet<MangaOrderEntity> Orders { get; set; } = null!;
        public DbSet<UserMangaEntity> UserMangas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(DbConfig.ConfigureUserEntity);
            modelBuilder.Entity<MangaEntity>(DbConfig.ConfigureMangaEntity);
            modelBuilder.Entity<MangaOrderEntity>(DbConfig.ConfigureMangaOrderEntity);
            modelBuilder.Entity<MangaOrderItemEntity>(DbConfig.ConfigureMangaOrderItemEntity);
            modelBuilder.Entity<UserMangaEntity>(DbConfig.ConfigureUserMangaEntity);

            base.OnModelCreating(modelBuilder);
        }
    }
}

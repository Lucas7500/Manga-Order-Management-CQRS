using Microsoft.EntityFrameworkCore;

namespace ProjetoRabbitMQ.DB
{
    public sealed class MySqlContext(DbContextOptions<MySqlContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Teste>(entityBuilder =>
            //{
            //    entityBuilder.HasKey(x => x.Id);

            //    entityBuilder
            //        .Property(x => x.Id)
            //        .ValueGeneratedNever()
            //        .HasConversion(
            //            ulid => ulid.ToString(),
            //            str => Ulid.Parse(str))
            //        .HasColumnType("VARCHAR(26)")
            //        .IsRequired();
            //});

            base.OnModelCreating(modelBuilder);
        }
    }
}

using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProjetoRabbitMQ.Bus;
using ProjetoRabbitMQ.DB;

namespace ProjetoRabbitMQ.Extensions
{
    public static class AppExtensions
    {
        public static void AddRabbitMQService(this IHostApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumer<RelatorioSolicitadoEventConsumer>();

                busConfigurator.UsingRabbitMq((context, config) =>
                {
                    config.Host(new Uri(builder.Configuration["RABBITMQ_URI"]!), host =>
                    {
                        host.Username(builder.Configuration["RABBITMQ_USERNAME"]!);
                        host.Password(builder.Configuration["RABBITMQ_PASSWORD"]!);
                    });

                    config.ConfigureEndpoints(context);
                });
            });
        }

        public static void AddDatabaseContext(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<MySqlContext>(opt =>
            {
                var connectionString = builder.Configuration["MY_SQL_CONNECTION_STRING"];
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }

        public static void EnsureDatabaseCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MySqlContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}

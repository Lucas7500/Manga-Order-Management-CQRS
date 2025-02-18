using MassTransit;
using ProjetoRabbitMQ.Relatorios;

namespace ProjetoRabbitMQ.Controllers
{
    public static class ApiEndpoints
    {
        public static void AddApiEndpoints(this WebApplication app)
        {
            app.MapPost("solicitar-relatorio/{name}", async (string name, IBus bus) =>
            {
                var solicitacao = new SolicitacaoRelatorio()
                {
                    Id = Guid.NewGuid(),
                    Nome = name,
                    Status = "Pendente",
                    ProcessedTime = null
                };

                Lista.Relatorios.Add(solicitacao);

                var eventRequest = new RelatorioSolicitadoEvent(solicitacao.Id, solicitacao.Nome);

                await bus.Publish(eventRequest);

                return Results.Ok(solicitacao);
            });

            app.MapGet("relatorios", () => Lista.Relatorios);
        }
    }
}

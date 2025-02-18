using MassTransit;
using ProjetoRabbitMQ.Relatorios;

namespace ProjetoRabbitMQ.Bus
{
    public class RelatorioSolicitadoEventConsumer(ILogger<RelatorioSolicitadoEventConsumer> logger) : IConsumer<RelatorioSolicitadoEvent>
    {
        public async Task Consume(ConsumeContext<RelatorioSolicitadoEvent> context)
        {
            logger.LogInformation("Processando Relatório Id:{Id}, Nome:{Nome}", context.Message.Id, context.Message.Nome);

            await Task.Delay(10000);

            var relatorio = Lista.Relatorios.FirstOrDefault(x => x.Id == context.Message.Id);
            if (relatorio != null)
            {
                relatorio.Status = "Completado";
                relatorio.ProcessedTime = DateTime.Now;
            }

            logger.LogInformation("Relatório Processado Id:{Id}, Nome:{Nome}", context.Message.Id, context.Message.Nome);
        }
    }
}

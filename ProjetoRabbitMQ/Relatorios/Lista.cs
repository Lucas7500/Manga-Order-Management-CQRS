namespace ProjetoRabbitMQ.Relatorios
{
    public static class Lista
    {
        public static List<SolicitacaoRelatorio> Relatorios = [];
    }

    public class SolicitacaoRelatorio
    {
        public Ulid Id { get; set; }
        public required string Nome { get; set; }
        public required string Status { get; set; }
        public DateTime? ProcessedTime { get; set; }
    }
}
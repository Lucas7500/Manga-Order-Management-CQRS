namespace ProjetoRabbitMQ.Models.Base
{
    public abstract class Product
    {
        public Guid Id { get; init; }
        public uint Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
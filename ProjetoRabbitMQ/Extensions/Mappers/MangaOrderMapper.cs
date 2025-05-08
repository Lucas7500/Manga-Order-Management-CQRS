using ProjetoRabbitMQ.Models.Enums;
using ProjetoRabbitMQ.Models.MangaOrder;
using ProjetoRabbitMQ.Models.MangaOrder.Commands;

namespace ProjetoRabbitMQ.Extensions.Mappers
{
    public static class MangaOrderMapper
    {
        public static MangaOrderEntity ToEntity(this RequestMangaOrderCommand command)
        {
            return new MangaOrderEntity
            {
                Id = Ulid.NewUlid(),
                CustomerId = command.CustomerId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                OrderedMangas = command.OrderedMangas.Select(manga => new MangaOrderItemEntity
                {
                    MangaId = manga.MangaId,
                    Quantity = manga.Quantity
                }).ToList(),
            };
        }
    }
}

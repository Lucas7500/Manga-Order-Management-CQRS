using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.MangaOrder.Queries;
using ProjetoRabbitMQ.Models.MangaOrder.Responses;

namespace ProjetoRabbitMQ.Models.MangaOrder.Handlers
{
    public class GetMangaOrderByIdQueryHandler : IRequestHandler<GetMangaOrderByIdQuery, Result<MangaOrderQueryModel>>
    {
        public Task<Result<MangaOrderQueryModel>> Handle(GetMangaOrderByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.MangaOrder.Queries;
using ProjetoRabbitMQ.Models.MangaOrder.Responses;

namespace ProjetoRabbitMQ.Models.MangaOrder.Handlers
{
    public class GetAllMangaOrderQueryHandler : IRequestHandler<GetAllMangaOrderQuery, Result<List<MangaOrderQueryModel>>>
    {
        public Task<Result<List<MangaOrderQueryModel>>> Handle(GetAllMangaOrderQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

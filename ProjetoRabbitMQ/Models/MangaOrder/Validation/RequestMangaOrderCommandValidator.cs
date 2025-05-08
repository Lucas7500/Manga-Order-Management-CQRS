using FluentValidation;
using ProjetoRabbitMQ.Models.MangaOrder.Commands;

namespace ProjetoRabbitMQ.Models.MangaOrder.Validation
{
    public class RequestMangaOrderCommandValidator : AbstractValidator<RequestMangaOrderCommand>
    {
        public RequestMangaOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("CustomerId is required.");
 
            RuleFor(x => x.OrderedMangas)
                .NotEmpty()
                .WithMessage("OrderedMangas is required.")
                .Must(mangas => mangas.Any())
                .WithMessage("At least one manga must be ordered.");
        }
    }
}

using FluentValidation;
using MediatR;
using ProjetoRabbitMQ.Extensions.Mappers;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Commands;
using ProjetoRabbitMQ.Models.Manga.Responses;

namespace ProjetoRabbitMQ.Models.Manga.Handlers
{
    public class CreateMangaCommandHandler(
        ILogger<CreateMangaCommandHandler> logger,
        IValidator<CreateMangaCommand> validator,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateMangaCommand, Result<CreatedMangaResult>>
    {
        public async Task<Result<CreatedMangaResult>> Handle(CreateMangaCommand request, CancellationToken ct)
        {
            logger.LogInformation("Handling Creation of Manga with title: {Title}", request.Title);

            var validationResult = await validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
            {
                return Result<CreatedMangaResult>.Failure(validationResult.ToString());
            }

            var repository = unitOfWork.MangaRepository;
            var mangaInDb = await repository.GetAsync(manga =>
                (manga.Title == request.Title || manga.Aliases.Contains(request.Title))
                && manga.Author == request.Author, ct);

            if (mangaInDb != null)
            {
                return Result<CreatedMangaResult>.Failure("This manga is already registered as {MangaName}!", mangaInDb.Title);
            }

            var manga = request.ToEntity();

            await repository.AddAsync(manga, ct);
            await unitOfWork.CommitAsync(ct);

            logger.LogInformation("Manga with title: {Title} created successfully", request.Title);

            return Result<CreatedMangaResult>.Success(new CreatedMangaResult(manga.Id));
        }
    }
}

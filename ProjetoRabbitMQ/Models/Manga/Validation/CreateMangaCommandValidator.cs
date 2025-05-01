using FluentValidation;
using ProjetoRabbitMQ.Models.Manga.Commands;

namespace ProjetoRabbitMQ.Models.Manga.Validation
{
    public class CreateMangaCommandValidator : AbstractValidator<CreateMangaCommand>
    {
        public CreateMangaCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .Length(1, 100)
                .WithMessage("Title must be between 1 and 100 characters.");

            RuleFor(x => x.Author)
                .NotEmpty()
                .WithMessage("Author is required.")
                .Length(1, 50)
                .WithMessage("Author must be between 1 and 50 characters.");

            RuleFor(x => x.ReleaseDate)
                .NotEmpty()
                .WithMessage("Release date is required.");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price is required.")
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description must be less than 500 characters.");

            RuleFor(x => x.Genres)
                .NotEmpty()
                .WithMessage("At least one genre is required.")
                .Must(genres => genres.Count <= 5)
                .WithMessage("You can specify a maximum of 5 genres.");

            RuleFor(x => x.Aliases)
                .NotEmpty()
                .WithMessage("At least one alias is required.")
                .Must(aliases => aliases.Count <= 5)
                .WithMessage("You can specify a maximum of 5 aliases.");
        }
    }
}

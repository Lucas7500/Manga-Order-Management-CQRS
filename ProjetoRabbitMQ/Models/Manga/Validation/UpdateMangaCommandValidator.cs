using FluentValidation;
using ProjetoRabbitMQ.Models.Manga.Commands;

namespace ProjetoRabbitMQ.Models.Manga.Validation
{
    public class UpdateMangaCommandValidator : AbstractValidator<UpdateMangaCommand>
    {
        public UpdateMangaCommandValidator()
        {
            When(x => x.Title != null, () =>
            {
                RuleFor(x => x.Title)
                     .NotEmpty()
                     .WithMessage("Title is required.")
                     .Length(1, 100)
                     .WithMessage("Title must be between 1 and 100 characters.");
            });
            
            When(x => x.Author != null, () =>
            {
                RuleFor(x => x.Author)
                    .NotEmpty()
                    .WithMessage("Author is required.")
                    .Length(1, 50)
                    .WithMessage("Author must be between 1 and 50 characters.");
            });
            
            When(x => x.ReleaseDate.HasValue, () =>
            {
                RuleFor(x => x.ReleaseDate)
                    .NotEmpty()
                    .WithMessage("Release date is required.");
            });
            
            When(x => x.Price.HasValue, () =>
            {
                RuleFor(x => x.Price)
                    .NotEmpty()
                    .WithMessage("Price is required.")
                    .GreaterThan(0)
                    .WithMessage("Price must be greater than 0.");
            });
            
            When(x => x.Description != null, () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(500)
                    .WithMessage("Description must be less than 500 characters.");
            });
            
            When(x => x.Genres != null, () =>
            {
                RuleFor(x => x.Genres)
                    .NotEmpty()
                    .WithMessage("At least one genre is required.")
                    .Must(genres => genres!.Count <= 5)
                    .WithMessage("You can specify a maximum of 5 genres.");
            });
            
            When(x => x.Aliases != null, () =>
            {
                RuleFor(x => x.Aliases)
                    .NotEmpty()
                    .WithMessage("At least one alias is required.")
                    .Must(aliases => aliases!.Count <= 5)
                    .WithMessage("You can specify a maximum of 5 aliases.");
            });
        }
    }
}

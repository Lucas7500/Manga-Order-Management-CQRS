using FluentValidation;
using ProjetoRabbitMQ.Models.User.Commands;

namespace ProjetoRabbitMQ.Models.User.Validation
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            When(command => !string.IsNullOrWhiteSpace(command.NewName), () =>
            {
                RuleFor(command => command.NewName)
                    .NotEmpty()
                    .WithMessage("Name is required")
                    .MaximumLength(100)
                    .WithMessage("Name must be less than 100 characters");
            });

            When(command => !string.IsNullOrWhiteSpace(command.NewEmail), () =>
            {
                RuleFor(command => command.NewEmail)
                    .NotEmpty()
                    .WithMessage("Email is required")
                    .EmailAddress()
                    .WithMessage("Email is invalid");
            });
            
            When(command => !string.IsNullOrWhiteSpace(command.NewPassword), () =>
            {
                RuleFor(command => command.NewPassword)
                    .NotEmpty()
                    .WithMessage("Password is required")
                    .MinimumLength(8)
                    .WithMessage("Password must be at least 8 characters");
            });
            
            When(command => command.NewRole.HasValue, () =>
            {
                RuleFor(command => command.NewRole)
                    .IsInEnum()
                    .WithMessage("Role is invalid");
            });
        }
    }
}

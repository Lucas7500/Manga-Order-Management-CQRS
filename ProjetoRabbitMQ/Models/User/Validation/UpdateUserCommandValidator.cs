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

            When(command => command.NewName != null, () =>
            {
                RuleFor(command => command.NewName)
                    .NotEmpty()
                    .WithMessage("Name is required")
                    .MaximumLength(100)
                    .WithMessage("Name must be less than 100 characters");
            });

            When(command => command.NewEmail != null, () =>
            {
                RuleFor(command => command.NewEmail)
                    .NotEmpty()
                    .WithMessage("Email is required")
                    .EmailAddress()
                    .WithMessage("Email is invalid");
            });
            
            When(command => command.NewPassword != null, () =>
            {
                RuleFor(command => command.NewPassword)
                    .NotEmpty()
                    .WithMessage("Password is required")
                    .MinimumLength(8)
                    .WithMessage("Password must be at least 8 characters");
            });
            
            When(command => command.NewRole != null, () =>
            {
                RuleFor(command => command.NewRole)
                    .IsInEnum()
                    .WithMessage("Role is invalid");
            });
        }
    }
}

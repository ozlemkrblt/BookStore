using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateUser;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>

{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(0);
        RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(0);
        RuleFor(command => command.Model.Email).NotEmpty().EmailAddress();
        RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(6);
    }
}
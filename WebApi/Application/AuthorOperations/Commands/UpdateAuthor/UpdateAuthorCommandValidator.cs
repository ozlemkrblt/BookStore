using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {

        RuleFor(command => command.authorId).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty();
        RuleFor(command => command.Model.BirthDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(command => command.Model.Bio).MaximumLength(100);

    }
}
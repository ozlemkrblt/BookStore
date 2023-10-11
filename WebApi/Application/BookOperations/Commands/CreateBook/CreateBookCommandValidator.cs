using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Application.BookOperations.Commands.CreateBook;
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
    public CreateBookCommandValidator() {
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
        RuleFor(command => command.Model.TotalPages).GreaterThan(0);
        RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(3);


    }
}


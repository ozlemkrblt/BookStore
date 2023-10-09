using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {

        public DeleteBookCommandValidator() { 

            RuleFor(command => command.bookId).NotEmpty().GreaterThan(0);
        } 
    }
}

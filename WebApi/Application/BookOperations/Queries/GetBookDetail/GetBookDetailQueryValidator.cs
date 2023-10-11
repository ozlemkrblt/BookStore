using FluentValidation;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator()
    {
        RuleFor(query => query.BookId).NotEmpty().GreaterThan(0);

    }
}




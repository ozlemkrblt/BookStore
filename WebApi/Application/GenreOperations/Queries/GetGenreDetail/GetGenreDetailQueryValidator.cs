using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
{
public GetGenreDetailQueryValidator(){

RuleFor(query => query.genreId).NotEmpty().GreaterThan(0);

}
}
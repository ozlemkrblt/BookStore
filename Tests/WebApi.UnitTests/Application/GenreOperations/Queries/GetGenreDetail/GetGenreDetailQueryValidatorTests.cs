using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidIdisGiven_Validator_ShouldReturnError(int id)
    {
        //arrange
        GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
        query.genreId = id;

        //act
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidIdisGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
        int genreId = 2;
        query.genreId = genreId;

        //act

        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Should().BeEmpty();
    }
}

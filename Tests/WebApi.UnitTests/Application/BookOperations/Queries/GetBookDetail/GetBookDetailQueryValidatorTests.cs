using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidIdisGiven_Validator_ShouldReturnError(int id)
    {
        //arrange
        GetBookDetailQuery query = new GetBookDetailQuery(null, null);
        query.BookId = id;

        //act
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidIdisGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        GetBookDetailQuery query = new GetBookDetailQuery(null, null);
        int BookId = 2;
        query.BookId = BookId;

        //act
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Should().BeEmpty();
    }
}

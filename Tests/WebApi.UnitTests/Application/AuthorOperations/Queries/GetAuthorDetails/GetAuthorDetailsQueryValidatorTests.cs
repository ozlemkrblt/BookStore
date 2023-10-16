using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetails;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetails;

public class GetAuthorDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidIdisGiven_Validator_ShouldReturnError(int id)
    {
        //arrange
        GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(null, null);
        query.AuthorId = id;

        //act
        GetAuthorDetailsQueryValidator validator = new GetAuthorDetailsQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidIdisGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(null, null);
        int authorId = 2;
        query.AuthorId = authorId;

        //act

        GetAuthorDetailsQueryValidator validator = new GetAuthorDetailsQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Should().BeEmpty();
    }
}

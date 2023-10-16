using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    private readonly BookStoreDbContext context;

    public DeleteGenreCommandValidatorTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidIdisGiven_Validator_ShouldReturnError(int id)
    {
        //arrange
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = id;

        //act
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputAreGiven_Validator_ShouldNotReturnError()
    {


        DeleteGenreCommand command = new DeleteGenreCommand(context);
        var genre = new Genre()
        {
            Name = "Fantasy",
        };
        context.Genres.Add(genre);
        context.SaveChanges();

        command.GenreId = genre.Id;

        //act
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Should().BeEmpty();
    }
}

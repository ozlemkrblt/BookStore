using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenNonExistingGenreIdGiven_InvalidOperationException_ShouldReturn()
    {
        var nonExistingGenreId = int.MaxValue;
        DeleteGenreCommand command = new DeleteGenreCommand(context);
        command.GenreId = nonExistingGenreId;

        //act & assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre to delete is not found!");
    }

    [Fact]
    public void WhenValidInputAreGiven_Genre_ShouldBeDeleted()
    {

        //arrange
        DeleteGenreCommand command = new DeleteGenreCommand(context);
        var genre = new Genre(){Name = "Fiction"};
        context.Genres.Add(genre);
        context.SaveChanges();

        command.GenreId = genre.Id;

        //act&assert
        FluentActions.Invoking(() => command.Handle()).Should().NotThrow(); ;

        //assert
        var deletedGenre = context.Books.SingleOrDefault(g => g.Id == genre.Id);
        deletedGenre.Should().BeNull();


    }

}



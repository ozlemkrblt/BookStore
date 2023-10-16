using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook;
public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenNonExistingGenreIdisGiven_InvalidOperationException_ShouldReturn()
    {
        //arrange
        var nonExistingGenreId = int.MaxValue;

        UpdateGenreCommand command = new UpdateGenreCommand(context);
        command.GenreId = nonExistingGenreId;

        //act & assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre not found!");
    }

    [Theory]
    [InlineData("Science Fiction")]
    [InlineData("Personal Growth")]
    [InlineData("Novel")]
    [InlineData("Foreign")]
    public void WhenAlreadyExistingGenreisGiven_InvalidOperationException_ShouldReturn(string name)
    {
        //arrange
        UpdateGenreCommand command = new UpdateGenreCommand(context);
        var model = new UpdateGenreModel() { Name = name };
        command.GenreId = context.Genres
                    .Where(g => g.Name == name)
                    .Select(g => g.Id)
                    .SingleOrDefault();
        command.model=model;

        //act & assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre already exists with a different id.");
    }



    [Fact]
    public void WhenValidInputAreGiven_Genre_ShouldBeUpdated()
    {
        // Arrange
        UpdateGenreCommand command = new UpdateGenreCommand(context);

        command.GenreId = 2;
        UpdateGenreModel model = new UpdateGenreModel() { Name = "Best seller"};
        command.model = model;


        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var updatedGenre = context.Genres.SingleOrDefault(genre => genre.Name == model.Name);
        updatedGenre.Should().NotBeNull();
        updatedGenre.Name.Should().Be(model.Name);


    }

}

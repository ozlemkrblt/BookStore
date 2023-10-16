using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre;
public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenAlreadyExistingGenreNameisGiven_InvalidOperationException_ShouldReturn()
    {
        //arrange
        var genre = new Genre() { Name = "Test_WhenAlreadyExistingGenreNameisGiven_InvalidOperationException_ShouldReturn" };
        context.Genres.Add(genre);
        context.SaveChanges();


        CreateGenreCommand command = new CreateGenreCommand(context);
        command.model = new CreateGenreModel() { Name = genre.Name };

        //act & assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre already exists!");
    }

    [Fact]
    public void WhenValidInputAreGiven_Genre_ShouldBeCreated()
    {

        //arrange
        CreateGenreCommand command = new CreateGenreCommand(context);
        CreateGenreModel model = new CreateGenreModel()
        {
            Name = "Best-seller",
        };

        command.model = model;

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //assert
        var genre = context.Genres.SingleOrDefault(genre => genre.Name == model.Name);
        genre.Should().NotBeNull();
        genre.Name.Should().Be(model.Name);



    }
}

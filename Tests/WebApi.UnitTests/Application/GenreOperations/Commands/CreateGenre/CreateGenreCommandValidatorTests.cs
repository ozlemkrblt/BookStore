using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre;
public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("Literature")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("Bes")]
    [InlineData("Best")]
    public void WhenInvalidNameIsGiven_Validator_ShouldReturnError(string name)
    {
        //arrange
        CreateGenreCommand command = new CreateGenreCommand(null);
        command.model = new CreateGenreModel() { Name = name };

        //act
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);


        //assert 
        result.Errors.Count.Should().BeGreaterThan(0);

    }
}


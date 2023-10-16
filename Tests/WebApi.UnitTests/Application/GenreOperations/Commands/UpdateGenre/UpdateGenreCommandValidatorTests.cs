using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;


namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidatorTests
{

    [Theory]
    [InlineData("F")]
    [InlineData("Fo")]
    [InlineData("For")]
    [InlineData("Fore")]
    [InlineData("Foreign")]
    public void WhenInvalidINameIsGiven_Validator_ShouldReturnErrors(string name)
    {
        //arrange
        UpdateGenreCommand command = new UpdateGenreCommand(null);
        command.model = new UpdateGenreModel()
        {
            Name = name
        };
        

        //act
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);


        //assert 
        result.Errors.Count.Should().BeGreaterThan(0);

    }
}


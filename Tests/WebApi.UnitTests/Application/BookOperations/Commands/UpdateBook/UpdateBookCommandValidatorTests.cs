using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook;
    public class UpdateBookCommandValidatorTests
    {

    [Theory]
    [InlineData("Lord of The Rings", 0, -1)]
    [InlineData("Lord of The Rings", 10000, 1)]
    [InlineData("Lord of The Rings", 1, -1)]
    [InlineData("", -1, -1)]
    [InlineData("", 100, 1)]
    [InlineData("", null, 1)]
    [InlineData("Lor", 11, 1)]
    [InlineData("Lor", null, 0)]
    [InlineData("Lord", 0, 1)] //right data
    [InlineData("Lord", 100, -1)]
    [InlineData(" ", 100, 1)]
    public void WhenInvalidInputAreGiven_Validator_ShouldReturnErrors( string title, int bookId, int genreId)
    {
        //arrange
        UpdateBookCommand command = new UpdateBookCommand(null, null);
        command.Model = new UpdateBookModel()
        {
            Title = title,
            GenreId = -genreId

        };

        command.bookId = bookId;

        //act
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);


        //assert 
        result.Errors.Count.Should().BeGreaterThan(0);

    }


    [Fact]
    public void WhenValidInputAreGiven_Validator_ShouldNotReturnError()
    {
        //arrange
        UpdateBookCommand command = new UpdateBookCommand(null, null);
        command.Model = new UpdateBookModel()
        {
            Title = "Lord of The Rings",
            GenreId = 3
        };

        //act
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}


using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook;
public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("Lord of The Rings", 0, -1)]
    [InlineData("Lord of The Rings", 0, 1)]
    [InlineData("Lord of The Rings", 1, -1)]
    [InlineData("", 0, -1)]
    [InlineData("", 100, 1)]
    [InlineData("", 0, 1)]
    [InlineData("Lor", 11, 1)]
    [InlineData("Lor", 0, 0)]
    [InlineData("Lord", 0, 1)] //right data
    [InlineData("Lord", 100, -1)]
    [InlineData(" ", 100, 1)]
    public void WhenInvalidInputAreGiven_Validator_ShouldReturnErrors(string title, int totalPages, int genreId)
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = title,
            TotalPages = totalPages,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = -genreId

        };

        //act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);


        //assert 
        result.Errors.Count.Should().BeGreaterThan(0);

    }

    [Fact]
    public void WhenDateTimeEqualNowisGiven_Validator_ShouldReturnError()
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = "Lord of The Rings",
            TotalPages = 100,
            PublishDate = DateTime.Now,
            GenreId = 3
        };

        //act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputAreGiven_Validator_ShouldNotReturnError()
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = "Lord of The Rings",
            TotalPages = 100,
            PublishDate = DateTime.Now.Date.AddYears(-3),
            GenreId = 3
        };

        //act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}


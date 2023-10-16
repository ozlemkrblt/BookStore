using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    private readonly BookStoreDbContext context;

    public DeleteBookCommandValidatorTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidIdisGiven_Validator_ShouldReturnError(int id)
    {
        //arrange
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.bookId= id;

        //act
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputAreGiven_Validator_ShouldNotReturnError()
    {
        

        DeleteBookCommand command = new DeleteBookCommand(context);
        var book = new Book()
        {
            Title = "Lord of The Rings",
            TotalPages = 100,
            PublishDate = DateTime.Now.Date.AddYears(-50),
            GenreId = 3
        };
        context.Books.Add(book);
        context.SaveChanges();

        command.bookId = book.Id;

        //act
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Should().BeEmpty();   
    }
}

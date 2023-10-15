using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenNotExistingBookIdGiven_InvalidOperationException_ShouldReturn()
    {
        var nonExistingBookId = int.MaxValue; 
        DeleteBookCommand command = new DeleteBookCommand(context);
        command.bookId= nonExistingBookId;

        //act & assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book to delete is not found!");
    }

    [Fact]
    public void WhenValidInputAreGiven_Book_ShouldBeDeleted()
    {

        //arrange
        DeleteBookCommand command = new DeleteBookCommand(context);
        var book=new Book()
        {
            Title = "Lord of The Rings",
            TotalPages = 1000,
            PublishDate = DateTime.Now.Date.AddYears(-50),
            GenreId = 3
        };
        context.Books.Add(book);
        context.SaveChanges();

        command.bookId = book.Id;

        //act&assert
        FluentActions.Invoking(() => command.Handle()).Should().NotThrow(); ;

        //assert
        var deletedBook = context.Books.SingleOrDefault(b => b.Id == book.Id);
        deletedBook.Should().BeNull();


    }

}



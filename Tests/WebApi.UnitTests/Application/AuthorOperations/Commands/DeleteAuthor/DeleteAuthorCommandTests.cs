using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenNonExistingAuthorIdGiven_InvalidOperationException_ShouldReturn()
    {
        var nonExistingAuthorId = int.MaxValue;
        DeleteAuthorCommand command = new DeleteAuthorCommand(context);
        command.authorId = nonExistingAuthorId;

        //act & assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author to delete is not found!");
    }

    [Fact]
    public void WhenAuthorWithABookIdisGiven_InvalidOperationException_ShouldReturn()
    {

        DeleteAuthorCommand command = new DeleteAuthorCommand(context);
        command.authorId = 1;

        //act & assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This author has a published book. If you want to delete author , please remove book first.");
    }


    [Fact]
    public void WhenValidInputAreGiven_Book_ShouldBeDeleted()
    {

        //arrange
        DeleteAuthorCommand command = new DeleteAuthorCommand(context);
        var author = new Author

        {
            Name = "Jane",
            Surname = "Austen",
            BirthDate = new DateTime(1775, 12, 16),
            Bio = "Jane Austen, English writer who first gave the novel its distinctly modern character through her treatment of ordinary people in everyday life. "


        };
        context.Authors.Add(author);
        context.SaveChanges();

        command.authorId = author.Id;

        //act&assert
        FluentActions.Invoking(() => command.Handle()).Should().NotThrow(); ;

        //assert
        var deletedAuthor = context.Books.SingleOrDefault(a => a.Id == author.Id);
        deletedAuthor.Should().BeNull();


    }

}



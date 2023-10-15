using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook;
public class UpdateBookCommandTests: IClassFixture<CommonTestFixture>
{ 
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenNotExistingBookIdGiven_InvalidOperationException_ShouldReturn()
    {
        //arrange
        var nonExistingBookId = int.MaxValue;

        UpdateBookCommand command = new UpdateBookCommand(context,mapper);
        command.bookId = nonExistingBookId;

        //act & assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book to update is not found!");
    }

    [Fact]
    public void WhenValidInputAreGiven_Book_ShouldBeUpdated()
    {
        // Arrange
        UpdateBookCommand command = new UpdateBookCommand(context, mapper);

        command.bookId = 2;
        UpdateBookModel model = new UpdateBookModel() { Title = "Fahrenheit 451", GenreId = 2 };
        command.Model = model;


        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var updatedBook = context.Books.SingleOrDefault(book => book.Title == model.Title);
        updatedBook.Should().NotBeNull();
        updatedBook.GenreId.Should().Be(model.GenreId);
        updatedBook.Title.Should().Be(model.Title);

        
    }


    //[Fact]
    public void WhenValidAuthorIdProvided_UpdateBook_Should_CorrectlyUpdateAuthors()
    {
        // Arrange
        UpdateBookCommand command= new UpdateBookCommand(context,mapper);
        var book = new Book()
        {
            Title = "Herland",
            TotalPages = 1000,
            PublishDate = DateTime.Now.Date.AddYears(-50),
            GenreId = 3
        };
        var author = new Author()
        {
            Name = "Charlotte",
            Surname = "Perkins Gilman",
            BirthDate = DateTime.Now.Date.AddYears(-50),
            Bio = "This is a test bio."

        };
        var author2 = new Author()
        {
            Name = "Charlotte",
            Surname = "Perkins",
            BirthDate = DateTime.Now.Date.AddYears(-10),
            Bio = "This is a test bio for 2nd author."

        };

        context.Books.Add(book);
        context.Authors.Add (author);
        context.SaveChanges();


        //Create model
        var model = new UpdateBookModel() { Title="Herland" , GenreId=2, AuthorIds = {author.Id, author2.Id} };
        command.bookId = book.Id;
        command.Model= model;


        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var authorList = book.Authors.ToList();
        authorList.Should().HaveCount(2);   
        foreach ( Author a in authorList)
        {
            a.Name.Should().Be(author.Name);
            a.Surname.Should().Be(author.Surname);
            a.Bio.Should().Be(author.Bio);
            a.BirthDate.Should().Be(author.BirthDate);
        }

    }

   

}

using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
{ 
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldReturn()
    {
        //arrange
        var book = new Book() { Title = "Test_WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldReturn", TotalPages = 100, PublishDate = new System.DateTime(1990, 01, 10), GenreId = 3 };
        context.Books.Add(book);
        context.SaveChanges();


        CreateBookCommand command = new CreateBookCommand(context,mapper);
        command.Model = new CreateBookModel() {Title= book.Title };

        //act & assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book already exists!");
    }

    [Fact]
    public void WhenValidInputAreGiven_Book_ShouldBeCreated()
    {

        //arrange
        CreateBookCommand command = new CreateBookCommand(context, mapper);
        CreateBookModel model = new CreateBookModel()
        {
            Title = "Hobbit",
            TotalPages = 1000,
            PublishDate = DateTime.Now.Date.AddYears(-50),
            GenreId = 3
        };

        command.Model = model;

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //assert
        var book = context.Books.SingleOrDefault(book => book.Title == model.Title);
        book.Should().NotBeNull();
        book.TotalPages.Should().Be(model.TotalPages);
        book.PublishDate.Should().Be(model.PublishDate);
        book.GenreId.Should().Be(model.GenreId);
        book.Title.Should().Be(model.Title);

    }
}
